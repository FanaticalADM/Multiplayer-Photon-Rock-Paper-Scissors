using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviourPunCallbacks
{
    [field: SerializeField] public float Speed { get; set; }
    [field: SerializeField] public float BasicSpeed { get; private set; }
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] public Vector3 CurrentDirection { get; private set; }
    Rigidbody2D playerRb;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        Speed = BasicSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            GetInput();
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
        CurrentDirection = playerRb.velocity.normalized;
    }
}
