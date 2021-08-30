using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;  
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1)
        {
            StartCoroutine(InstantiatePlayerManagerCoroutine());
        }
    }

    IEnumerator InstantiatePlayerManagerCoroutine()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.Instantiate("PlayerManager", Vector3.zero, Quaternion.identity);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Destroy(gameObject);
    }
}
