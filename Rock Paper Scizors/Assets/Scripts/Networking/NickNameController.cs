using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class NickNameController : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;

    private string playerName;
    private string playerNameFile = "playerNameFile";

    void Start()
    {
        nameInputField = GetComponent<TMP_InputField>();
        nameInputField.text = PlayerPrefs.GetString(playerNameFile);
        SetPlayerName();
    }

    public void SetPlayerName()
    {
        playerName = nameInputField.text.ToString();
        PhotonNetwork.NickName = playerName;
        PlayerPrefs.SetString(playerNameFile, playerName);
    }

    private void OnEnable()
    {
        nameInputField.text = PlayerPrefs.GetString(playerNameFile);
    }
}
