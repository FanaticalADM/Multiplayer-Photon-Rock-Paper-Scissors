using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ScoreManager : MonoBehaviourPunCallbacks
{
    private static ScoreManager instance;
    public static ScoreManager Instance { get { return instance; } set {instance = value; } }

    public int topScore;
    public List<ScorePlayerLabelController> playerLabels;
    public GameObject scoresPanel;

    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject scorePlayerLabel;

    private int numberOfPlayers = 0;

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
    }

    void Start()
    {
        scorePanel.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToogleScoreBoard();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ToogleScoreBoard();
        }

        if(playerLabels.Count != numberOfPlayers)
        {
            numberOfPlayers = playerLabels.Count;
            photonView.RPC("RPC_SortScores", RpcTarget.AllBuffered);
        }
    }

    [PunRPC] public void RPC_SortScores()
    {
        for (int i = 0; i < playerLabels.Count - 1; i++)
        {
            int maxScore = -1;
            int maxIndex = 0;
            for (int j = i; j < playerLabels.Count; j++)
            {
                if (playerLabels[j].playerManager.Score > maxScore)
                {
                    maxScore = playerLabels[j].playerManager.Score;
                    maxIndex = j;
                }
            }
            if(i == 0)
            {
                topScore = maxScore;
            }
            ScorePlayerLabelController temp = playerLabels[i];
            playerLabels[i] = playerLabels[maxIndex];
            playerLabels[maxIndex] = temp;
            SetupOnList(playerLabels[i], i);
        }
        SetupOnList(playerLabels[playerLabels.Count - 1], playerLabels.Count - 1);
    }

    private void ToogleScoreBoard()
    {
        scorePanel.SetActive(!scorePanel.activeInHierarchy); 
    }

    private void SetupOnList(ScorePlayerLabelController player, int position)
    {
        player.gameObject.transform.SetSiblingIndex(position);
        player.photonView.RPC("RPC_UpdatePosition", RpcTarget.AllBuffered);
    }
}
