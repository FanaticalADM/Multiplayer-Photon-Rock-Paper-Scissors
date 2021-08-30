using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WallAbility : Ability
{
    [SerializeField] private GameObject wall;
    [SerializeField] private float wallDistance;
    [SerializeField] private float wallDestructionTime;

    void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    public override void Activate()
    {
        if (activeAbility == true)
        {
            Debug.Log("Activating Wall");
            playerController.photonView.RPC("RPC_CreateWall", RpcTarget.AllBuffered, wallDistance, wallDestructionTime);
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
}
