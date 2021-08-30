using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class TransformationAbility : Ability
{
    private int intNextForm;

    private void Awake()
    {
        playerController = gameObject.GetComponent<PlayerController>();
    }

    private void SetupTransformation()
    {
        intNextForm = playerController.FormIndex;
        SetNextFormInt();
    }

    public override void Initialize()
    {
        CooldownTime = Cooldown;
        SetupTransformation();
        StartCoroutine(WaitToActivateCoroutine());
    }

    public override void InGameInitialize()
    {  
    }

    public override void Activate()
    {
        if (activeAbility == true)
        {
            playerController.photonView.RPC("RPC_ChangeForm", RpcTarget.AllBuffered, intNextForm);
            SetNextFormInt();
            StartCoroutine(WaitToActivateCoroutine());
            CooldownTime = Cooldown;
        }
    }

    void SetNextFormInt()
    {
        intNextForm += Random.Range(0, 2) == 0 ? -1 : 1;
        intNextForm = intNextForm == -1 ? 2 : intNextForm;
        intNextForm = intNextForm == 3 ? 0 : intNextForm;
        AbilityName = ((FormStateEnum)intNextForm).ToString();
        playerController.leftAbility.abilityName.text = AbilityName;
    }
}
