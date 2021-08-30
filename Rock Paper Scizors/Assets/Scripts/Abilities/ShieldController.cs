using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
            ShieldWithGemCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
            ShieldWithGemCollision(collision);
    }

    void ShieldWithGemCollision(Collision2D collision)
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
