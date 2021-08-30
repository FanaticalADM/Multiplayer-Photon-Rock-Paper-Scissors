using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField createInputField;
    [SerializeField] private TMP_InputField joinInputField;
    [SerializeField] private TextMeshProUGUI errorText;

    public void CreateRoom()
    {
        if (HasCodeBeenProvided(createInputField))
        {
            PhotonNetwork.CreateRoom(GetCode(createInputField));
        }       
    }

    public void JoinRoom()
    {
        if (HasCodeBeenProvided(joinInputField))
        {
            PhotonNetwork.JoinRoom(GetCode(joinInputField));
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Level1");
    }

    public bool HasCodeBeenProvided(TMP_InputField inputField)
    {
        string potentialGameCode = inputField.text;
        if (string.IsNullOrEmpty(potentialGameCode))
        {
            errorText.text = "You need to provide game code first.";
            errorText.gameObject.SetActive(true);
            return false;
        }
        errorText.gameObject.SetActive(false);
        return true;
    }

    public string GetCode(TMP_InputField inputField)
    {
        string gameCode = inputField.text;
        return gameCode;
    }

    public void CreateDefaultRoom()
    {
        PhotonNetwork.CreateRoom("Default");
    }

    public void JoinDefaultRoom()
    {
        PhotonNetwork.JoinRoom("Default");
    }
}
