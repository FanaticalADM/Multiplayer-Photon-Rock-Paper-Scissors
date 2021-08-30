using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } private set { instance = value; } }

    public int pointLimit;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        StartGame();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.Disconnect();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        PhotonNetwork.LeaveRoom();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
    }

    public void StopGame()
    {
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator WaitToRestartCoroutine(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        RestartGame();
    }

}
