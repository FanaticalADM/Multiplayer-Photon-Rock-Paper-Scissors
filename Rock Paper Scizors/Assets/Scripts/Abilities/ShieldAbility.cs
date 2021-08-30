using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ShieldAbility : Ability
{
    [SerializeField] private float shieldTime;

    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    public override void Activate()
    {
        if (activeAbility == true)
        {
            Debug.Log("Activating Shield");
            playerController.photonView.RPC("RPC_ChangeShieldStatus", RpcTarget.AllBuffered, true);
            StartCoroutine(ShieldCoroutine());
            activeAbility = false;
        }  
    }

    public override void InGameInitialize()
    {
    }

    public override void Initialize()
    {
        CooldownTime = Cooldown;
        StartCoroutine(WaitToActivateCoroutine());
    }

    IEnumerator ShieldCoroutine()
    {
        yield return new WaitForSeconds(shieldTime);
        playerController.photonView.RPC("RPC_ChangeShieldStatus", RpcTarget.AllBuffered, false);
        Debug.Log("End of Shield");
    }
}
