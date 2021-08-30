using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LobbyUIController : MonoBehaviour
{
    [SerializeField] private GameObject basicMenu;
    [SerializeField] private GameObject customMenu;
    [SerializeField] private TMP_InputField nameInputField;

    private string playerName;
    private string playerNameFile = "playerNameFile";

    // Start is called before the first frame update
    void Start()
    {
        basicMenu.SetActive(true);
        customMenu.SetActive(false);
        nameInputField.text = PlayerPrefs.GetString(playerNameFile);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ToogleCustumMenu()
    {
        customMenu.SetActive(!customMenu.activeInHierarchy);
    }

    public void ToogleBasicMenu()
    {
        basicMenu.SetActive(!basicMenu.activeInHierarchy);
    }

    public void SetPlayerName()
    {
        playerName = nameInputField.text.ToString();
        PlayerPrefs.SetString(playerNameFile, playerName);       
    }
}
