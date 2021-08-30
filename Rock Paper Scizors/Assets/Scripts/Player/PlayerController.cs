using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] public AbilityController leftAbility;
    [SerializeField] public AbilityController rightAbility;
    [SerializeField] public GameObject shield;
    [SerializeField] public GameObject wall;

    [SerializeField] private PlayerForm[] playerForms;
    [SerializeField] private GameObject ui;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Ability[] abilities;
    [SerializeField] private Ability transformationAbility;
    [SerializeField] private Ability randomAbility;
    [SerializeField] public KeyCode leftKey;
    [SerializeField] public KeyCode rightKey;

    private int previousFormIndex = -1;
    private float horizontalInput;
    private float verticalInput;
    private bool shieldStatus;
    private PlayerManager playerManager;
    private Rigidbody2D playerRb;
    private Vector3 lastPosition;
    private Vector3 opositeDirection;

    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float BasicSpeed { get; private set; }
    public Vector3 CurrentDirection { get; private set; }
    public int FormIndex { get; private set; }

    void Awake()
    {
        playerManager = PhotonView.Find((int)photonView.InstantiationData[0]).GetComponent<PlayerManager>();
        playerRb = GetComponent<Rigidbody2D>();
        Speed = BasicSpeed;
    }

    private void Start()
    {
        nameText.text = photonView.Owner.NickName;
        playerRb = GetComponent<Rigidbody2D>();
        if (photonView.IsMine)
        {
            photonView.RPC("RPC_ChangeForm", RpcTarget.AllBuffered, Random.Range(0, playerForms.Length));
            transformationAbility = abilities[0];
            randomAbility = abilities[Random.Range(1, abilities.Length)];
            for (int i = 0; i < abilities.Length; i++)
            {
                abilities[i].Initialize();
            }

            leftAbility.keyName.text = leftKey.ToString();
            rightAbility.keyName.text = rightKey.ToString();

            leftAbility.abilityName.text = transformationAbility.AbilityName;
            rightAbility.abilityName.text = randomAbility.AbilityName;

            leftAbility.Initialize(transformationAbility, leftKey);
            rightAbility.Initialize(randomAbility, rightKey);

        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(ui);
            Destroy(playerRb);
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            GetInput();

            if (Input.GetKeyDown(leftKey))
            {
                UseTransformationAbility();
            }
            if (Input.GetKeyDown(rightKey))
            {
                UseRandomAbility();
            }
        }
        
        if(lastPosition != transform.position)
        {
            CheckDirection();
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            Move();
        }
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        playerRb.velocity = new Vector3(horizontalInput, verticalInput, 0) * Speed;
        if (playerRb.velocity.magnitude > 0)
            CurrentDirection = playerRb.velocity.normalized;
    }

    public void UseTransformationAbility()
    {
        leftAbility.abilityName.text = transformationAbility.AbilityName;
        transformationAbility.Activate();
    }

    public void UseRandomAbility()
    {
        if (randomAbility.activeAbility)
        {
            randomAbility.Activate();
            randomAbility = abilities[Random.Range(1, abilities.Length)];
            randomAbility.Initialize();
            rightAbility.Initialize(randomAbility);
        }
    }

    [PunRPC]
    public void RPC_ChangeForm(int _index)
    {
        if (_index == previousFormIndex)
            return;

        FormIndex = _index;
        playerForms[FormIndex].gameObject.SetActive(true);
        Debug.Log("Current Form is: " + playerForms[FormIndex].form.formName);
        if (previousFormIndex != -1)
        {
            playerForms[previousFormIndex].gameObject.SetActive(false);
        }

        previousFormIndex = FormIndex;

        if (photonView.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("formIndex", FormIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    [PunRPC]
    public void RPC_ChangeShieldStatus(bool status)
    {
        shieldStatus = status;
        shield.gameObject.SetActive(shieldStatus);
        if (photonView.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("shieldStatus", shieldStatus);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    [PunRPC]
    public void RPC_CreateWall(float wallDistance, float wallDestructionTime)
    {
        GameObject newWall = Instantiate(wall, transform.position + opositeDirection * wallDistance, Quaternion.Euler(0, 0, Mathf.Atan2(CurrentDirection.y, CurrentDirection.x) * 180 / Mathf.PI));
        newWall.SetActive(true);
        Destroy(newWall, wallDestructionTime);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (!photonView.IsMine && targetPlayer == photonView.Owner)
        {
            RPC_ChangeForm((int)changedProps["formIndex"]);
            RPC_ChangeShieldStatus((bool)changedProps["shieldStatus"]);
        }
    }

    public void Die()
    {
        playerManager.Die();
    }

    public void AddPoints(int points)
    {
        playerManager.photonView.RPC("RPC_AddScore", RpcTarget.AllBuffered, points);
    }

    public void ReduceCooldown(float ammount)
    {
        transformationAbility.CooldownTime -= Mathf.Clamp(transformationAbility.Cooldown * ammount, 0, transformationAbility.Cooldown);
        randomAbility.CooldownTime -= Mathf.Clamp(randomAbility.Cooldown*ammount,0, randomAbility.Cooldown);
    }

    private void CheckDirection()
    {
        opositeDirection = lastPosition - transform.position;
        opositeDirection.Normalize();
        lastPosition = transform.position;
    }
}
