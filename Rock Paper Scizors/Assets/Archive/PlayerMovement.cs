using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float normalMovementSpeed;
    [SerializeField] private float boostMovementSpeed;
    private float movementSpeed;

    private float horizontalInput;
    private float verticalInput;

    private bool isBoostSpeedOn;
    private Rigidbody2D playerRb;
    [SerializeField] private float maximumStamina;
    [SerializeField] private float stamina;
    [SerializeField] private float staminaRegenerationRate;

    private PhotonView photonView;

    private void Awake()
    {
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            GetInput();
            SetMovementSpeed();
            RegenerateStamina();
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
        isBoostSpeedOn = Input.GetKey(KeyCode.Space);
    }

    private void Move()
    {
        playerRb.velocity = new Vector3(horizontalInput, verticalInput, 0) * normalMovementSpeed;
    //    playerRb.MovePosition(transform.position
     //       + new Vector3(horizontalInput, verticalInput, 0) * normalMovementSpeed * Time.fixedDeltaTime);
    }

    private void SetMovementSpeed()
    {
        if (isBoostSpeedOn && stamina > 0)
        {
            movementSpeed = boostMovementSpeed;
            stamina -= Time.deltaTime;
        }
        else
        {
            movementSpeed = normalMovementSpeed;
        }

        ClampStamina();

    }

    private void ClampStamina()
    {
        if (stamina >= maximumStamina)
            stamina = maximumStamina;

        if (stamina <= 0)
            stamina = 0;
    }

    private void RegenerateStamina()
    {
        if (playerRb.velocity == Vector2.zero && stamina < maximumStamina) 
        {
            stamina += staminaRegenerationRate*Time.deltaTime;
        }
    }

}
