using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Pun;
using TMPro;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject ui;
    [SerializeField] private float spawnTime;
    [SerializeField] private TextMeshProUGUI scoreText;

    private GameObject player;
    private GameObject scorePlayerLabel;
    private ScorePlayerLabelController scorePlayerLabelController;
    private string scoreString = "Score: ";
    private bool updateScore;
    private string playerNameFile = "playerNameFile";

    [field: SerializeField] public string PlayerName { get; private set; }
    [field: SerializeField] public int Score { get; private set; }

    void Start()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("RPC_SetupPlayerName", RpcTarget.AllBuffered);
            Score = 0;
            CreatePlayer();
            CreateScoreLabel();
        }
        else
        {
            Destroy(ui);
        }

        updateScore = false;

        if (photonView.IsMine)
        {
            UpdateScore();
        }
    }

    private void Update()
    {
        if (updateScore == true && photonView.IsMine)
        {
            UpdateScore();
            updateScore = false;
        }
    }

    void UpdateScore()
    {
        scoreText.text = scoreString + Score;
        scorePlayerLabelController.photonView.RPC("RPC_UpdateScore", RpcTarget.AllBuffered);
    }

    void CreatePlayer()
    {
        Vector3 spawnPoint = PlayersSpawner.Instance.SpawnPlayer();
        player = PhotonNetwork.Instantiate("Player", spawnPoint, Quaternion.identity, 0, new object[] { photonView.ViewID });
    }

    void CreateScoreLabel()
    {
        scorePlayerLabel = PhotonNetwork.Instantiate("Player Label", Vector3.zero, Quaternion.identity, 0, new object[] { photonView.ViewID });
        scorePlayerLabelController = scorePlayerLabel.GetComponent<ScorePlayerLabelController>();
    }

    IEnumerator WaitToRespawnPlayer()
    {
        yield return new WaitForSeconds(spawnTime / 2);
        CreatePlayer();
    }

    [PunRPC] public void RPC_AddScore(int newScore)
    {
        Score += newScore;
        updateScore = true;
        ScoreManager.Instance.photonView.RPC("RPC_SortScores", RpcTarget.AllBuffered);
    }

    [PunRPC] public void RPC_SetupPlayerName()
    {
        PhotonNetwork.NickName = PlayerPrefs.GetString(playerNameFile);
        PlayerName = photonView.Owner.NickName;
    }

    [PunRPC] public void RPC_SpawnPlayer()
    {
        if (photonView.IsMine)
        {
          PlayersSpawner.Instance.SpawnPlayer(player);
        } 
    }

    public void Die()
    {
        PhotonNetwork.Destroy(player);
        StartCoroutine(WaitToRespawnPlayer());
    }
}
