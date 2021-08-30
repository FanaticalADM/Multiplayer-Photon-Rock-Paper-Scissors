using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;

[Serializable]
public abstract class FormState 
{ 
    public FormStateEnum _FormStateEnum { get; set; }
    public FormStateEnum _EnemyFormStateEnum { get; set; }
    public FormStateEnum _PreyFormStateEnum { get; set; }
    protected PhotonView photonView;
    protected CharacterForm characterForm;
    protected Color color;
    protected FormState currentCollisionFormState;
    protected PlayerManager playerManager;
    public List<FormState> OtherStates { get; protected set; } = new List<FormState>();

    public FormState(CharacterForm characterForm)
    {
        this.characterForm = characterForm;
        photonView = characterForm.gameObject.GetComponent<PhotonView>();
        playerManager = characterForm.gameObject.GetComponentInParent<PlayerManager>();
       // characterForm.SetForm(this);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") == false)
        {
            return;
        }

        FormState collisionFormState = collision.gameObject.GetComponent<CharacterForm>().CurrentForm;
        currentCollisionFormState = collision.gameObject.GetComponent<CharacterForm>().CurrentForm;
        if (photonView.IsMine)
        {
            Debug.Log($"Start of Collision: {_FormStateEnum} collided with {currentCollisionFormState._FormStateEnum}.");
        }

        //photonView.RPC("RPC_Collision", RpcTarget.All);


        if (collisionFormState._FormStateEnum == _EnemyFormStateEnum)
        {
            if (photonView.IsMine)
            {
                Debug.Log($"You were killed by {collisionFormState._FormStateEnum}");
            }
           // characterForm.Respawn();
           // collision.gameObject.GetComponent<PlayerManager>().AddScore(1);
        }
        else if (collisionFormState._FormStateEnum == _PreyFormStateEnum)
        {
            if (photonView.IsMine)
            {
                Debug.Log($"You killed {collisionFormState._FormStateEnum}");
            }
            playerManager.photonView.RPC("RPC_AddScore", RpcTarget.All, 1);// AddScore(1);
            collisionFormState.characterForm.PlayerManager.photonView.RPC("RPC_PlayerDeathAndRespanwn", RpcTarget.All);
            //collisionFormState.characterForm.PlayerManager.PlayerDeathAndRespanwn();
        }
 
    }

    public virtual void OnStateEnter()
    {
    }

    public virtual void ForcedSetup()
    {

    }

    public virtual void OnStateExit()
    {
    }

    public void CollisionHandler()
    {

    }
}
