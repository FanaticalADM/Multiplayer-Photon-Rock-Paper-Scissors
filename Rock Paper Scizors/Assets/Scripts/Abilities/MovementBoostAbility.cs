using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovementBoostAbility : Ability
{
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashSpeed = 25f;

    private void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    public override void Initialize()
    {
        CooldownTime = Cooldown;
        StartCoroutine(WaitToActivateCoroutine());
    }

    public override void InGameInitialize()
    {
    }

    public override void Activate()
    {
        if (activeAbility == true)
        {
            Debug.Log("Activating Dash");
            playerController.Speed = dashSpeed;
            StartCoroutine(DashCoroutine());
            activeAbility = false;
        }
    }

    IEnumerator DashCoroutine()
    {
        yield return new WaitForSeconds(dashTime);
        playerController.Speed = playerController.BasicSpeed;
        Debug.Log("End of Dash");
    }
}


