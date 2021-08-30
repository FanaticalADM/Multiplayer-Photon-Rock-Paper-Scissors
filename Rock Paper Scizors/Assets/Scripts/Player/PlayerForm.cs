using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerForm : MonoBehaviourPunCallbacks, IDamageable
{
    public Form form;
    public PlayerController playerController;

    private void Update()
    {
        if(photonView.IsMine)
        transform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(playerController.CurrentDirection.y, playerController.CurrentDirection.x) * 180 / Mathf.PI);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            PlayerWithPlayerCollision(collision);
        if (collision.gameObject.CompareTag("Gem"))
            PlayerWithGemCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            PlayerWithPlayerCollision(collision);
        if (collision.gameObject.CompareTag("Gem"))
            PlayerWithGemCollision(collision);
    }

    public void TakeDamage()
    {
        photonView.RPC("RPC_TakeDamage", RpcTarget.All);
    }

    [PunRPC] void RPC_TakeDamage()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        playerController.Die();
    }

    void PlayerWithPlayerCollision(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<IDamageable>() != null)
        {
            PlayerForm collisionForm = collision.gameObject.GetComponent<PlayerForm>();
            if (collisionForm.form._formStateEnum == form._enemyFormStateEnum)
            {
                collisionForm.playerController.AddPoints(1);
                TakeDamage();
            }
            else if(collisionForm.form._formStateEnum == form._preyFormStateEnum)
            {
            }
        }
    }

    void PlayerWithGemCollision(Collision2D collision)
    {
        Debug.Log("Collision with Gem");
        if (collision.gameObject.GetComponent<ICooldownReduction>() != null)
        {
            ReduceCooldown(collision.gameObject.GetComponent<ICooldownReduction>().ReductionProcentage);
            collision.gameObject.GetComponent<CooldownGemController>().photonView.RPC("DestroyGem", RpcTarget.AllBuffered, 0.0f);
        }
    }

    public void ReduceCooldown(float ammount)
    {
        playerController.ReduceCooldown(ammount);
    }
}
