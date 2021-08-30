using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GemSpawner : MonoBehaviourPunCallbacks
{
    private Vector2 radius;
    private float spawnTime = 10.0f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        radius = new Vector2(transform.localScale.x, transform.localScale.y);
        InvokeRepeating("SpawnGem", 0, spawnTime);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    void SpawnGem()
    {
        if (PhotonNetwork.IsMasterClient) 
        { 
            GameObject newGem = PhotonNetwork.Instantiate("Gem Cooldown Reset", transform.position + new Vector3(Random.Range(-radius.x, radius.x), Random.Range(-radius.y, radius.y), 0) / 3f, Quaternion.identity);
            newGem.GetComponent<CooldownGemController>().photonView.RPC("DestroyGem", RpcTarget.AllBuffered, spawnTime);
        }
    }
}
