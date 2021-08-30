using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class SpawnPoint : MonoBehaviourPunCallbacks, IPunObservable
{
    [field: SerializeField] public bool IsActive { get; set; }
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        IsActive = true;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            photonView.RPC("RPC_ChangeActiveStatus", RpcTarget.AllBuffered, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            photonView.RPC("RPC_ChangeActiveStatus", RpcTarget.AllBuffered, false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            photonView.RPC("RPC_ChangeActiveStatus", RpcTarget.AllBuffered, true);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.IsActive);
        }
        else
        {
            this.IsActive = (bool)stream.ReceiveNext();
        }
    }

    [PunRPC]
    public void RPC_ChangeActiveStatus(bool newStatus)
    {
        IsActive = newStatus;
    }
}
