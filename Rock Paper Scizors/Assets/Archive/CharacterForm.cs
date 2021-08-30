using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class CharacterForm : MonoBehaviourPunCallbacks, IPunObservable
{
    public List<Sprite> FormSprites;
    public Sprite CurrentSprite { get; set; }
    public FormState CurrentForm { get; set; }
    private SpriteRenderer spriteRenderer;
    public List<FormState> formStates = new List<FormState>();
    public int statusInfo;
    private TransformationAbility transformationAbility;
    public PlayerManager PlayerManager { get; private set; }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        formStates.Add(new RockState(this));
        formStates.Add(new ScissorsState(this));
        formStates.Add(new PaperState(this));
        FormSetup(GenerateRandomForm());
        Debug.Log("randomform is done");
    }

    private void Start()
    {
        PlayerManager = GetComponentInParent<PlayerManager>();
        
    }

    private void Update()
    {
        if (!photonView.IsMine) 
        UpdateForm();
    }

    void FormSetup(FormState newForm)
    {
        CurrentForm = newForm;
        CurrentForm.OnStateEnter();
        statusInfo = CurrentForm._FormStateEnum.GetHashCode();
        CurrentSprite = FormSprites[CurrentForm._FormStateEnum.GetHashCode()];
        Debug.Log($"Player {photonView.ViewID} Current Enum Form is {CurrentForm._FormStateEnum}.");
        if (photonView.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("statusInfo", statusInfo);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    public FormState GenerateRandomForm()
    {
        int randomIndex = UnityEngine.Random.Range(0, formStates.Count);
        return formStates[randomIndex];
    }

    public void GenerateRandomFormSetup()
    {
        FormSetup(GenerateRandomForm());
    }


    public void SetCharacterColor(Color color)
    {
    //    spriteRenderer.color = color;
    }

    public void SetCharacterSprite(int index)
    {
   //     spriteRenderer.sprite = FormSprites[index];
    }

    void UpdateForm()
    {
        if (statusInfo != CurrentForm._FormStateEnum.GetHashCode())
        {
            FormSetup(formStates[statusInfo]);
        }
    }

    //public void SetForm(FormState newForm)
    //{
    //    if (CurrentForm != null)
    //    {
    //        CurrentForm.OnStateExit();
    //    }

    //    CurrentForm = null;
    //    CurrentForm = newForm;


    //    if (CurrentForm != null)
    //    {
    //        CurrentForm.OnStateEnter();
    //    }

    //    statusInfo = CurrentForm._FormStateEnum.GetHashCode();
    //    Debug.Log($"Player {photonView.ViewID} Current Enum Form is {CurrentForm._FormStateEnum}.");

    //    //if (photonView.IsMine)
    //    //{
    //    //    Hashtable hash = new Hashtable();
    //    //    hash.Add("statusInfo", statusInfo);
    //    //    PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    //    //}
    //  //  OnFormChanged.Invoke();
    //}


    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (photonView.IsMine == false && targetPlayer == photonView.Owner)
        {
            FormSetup(formStates[(int)changedProps["statusInfo"]]);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(this.statusInfo);
        }
        else
        {
            this.statusInfo = (int)stream.ReceiveNext();
        }
    }
}
