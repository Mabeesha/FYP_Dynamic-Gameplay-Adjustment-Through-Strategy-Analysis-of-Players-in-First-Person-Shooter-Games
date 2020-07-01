using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Developer : Dinuka Chathuranga 

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 move_Direction;

    public float speed = 5f;

    private float gravity = 20f;
    public float jump_Force = 10f;

    private float vertical_Veocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();


    }

 

    // Update is called once per frame
    void Update()
    {
        moveThePlayer(); // calling move the player
    }
     
    void moveThePlayer()
    {
        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL),0f, Input.GetAxis(Axis.VERTICAL));
        move_Direction = transform.TransformDirection(move_Direction); 

        move_Direction *= speed * Time.deltaTime;  // multiply to smooth movement
        ApplyGravity();
        // move the charactor controller by passing vector3 type move_Direction
        characterController.Move(move_Direction);
        //print("Hori: "+ Input.GetAxis("Horizontal"));


    } // move the player

    void ApplyGravity()
    {
        
        vertical_Veocity -= gravity * Time.deltaTime;
        PlayerJump();
        move_Direction.y = vertical_Veocity * Time.deltaTime;
    } //ApplyGravity

    void PlayerJump()
    {
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertical_Veocity = jump_Force;
        }
    }


}
