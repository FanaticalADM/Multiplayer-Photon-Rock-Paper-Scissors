using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ScorePlayerLabelController : MonoBehaviourPunCallbacks
{
    public PlayerManager playerManager;
    [SerializeField] private TextMeshProUGUI positionLabel;
    [SerializeField] private TextMeshProUGUI nameLabel;
    [SerializeField] private TextMeshProUGUI scoreLabel;
    private int position;

    private void Awake()
    {
        playerManager = PhotonView.Find((int)photonView.InstantiationData[0]).GetComponent<PlayerManager>();
        transform.SetParent(ScoreManager.Instance.scoresPanel.transform);
        InitialSetup();
        ScoreManager.Instance.playerLabels.Add(this);
        photonView.RPC("RPC_UpdatePlayerName", RpcTarget.AllBuffered);
    }

    public void InitialSetup()
    {
        photonView.RPC("RPC_UpdatePosition", RpcTarget.AllBuffered);
    }

    [PunRPC]public void RPC_UpdatePosition()
    {
        position = transform.GetSiblingIndex() + 1;
        positionLabel.text = position.ToString();
    }

    [PunRPC] public void RPC_UpdateScore()
    {
        scoreLabel.text = playerManager.Score.ToString();
    }

    [PunRPC]
    public void RPC_UpdatePlayerName()
    {
        nameLabel.text = photonView.Owner.NickName;
    }
}
