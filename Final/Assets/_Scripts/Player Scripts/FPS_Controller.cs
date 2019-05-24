using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FPS_Controller : MonoBehaviour
{
    #region Local Variables
    [SerializeField]
    private float speed;
    private float jumpSpeed = 8.0F;
    private float gravity = 30.0F;
    private int standingHeight = 2, crouchingHeight = 1;
    [SerializeField]
    private bool crouching = false;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    #endregion Local Variables

    #region Variables accessed by other scripts
    public bool isMoving = false;
    private bool isSprinting = false;
    #endregion Variables accessed by other scripts



    void Start ()
    {
        speed = 6;
        controller = GetComponent<CharacterController>();
    }

    void Update ()
    {
        if (controller.isGrounded)
        {
            float horizontal, vertical;
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            moveDirection = new Vector3(horizontal, 0, vertical);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if ((Input.GetButton("Jump" )) && Time.timeScale != 0 && !crouching)
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);





        SpringHandler();
        CrouchHandler();

    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(controller.velocity.x) > 0.5f || Mathf.Abs(controller.velocity.z) > 0.5f)
            isMoving = true;      
        else
            isMoving = false;
    }

    public bool GetSprintStatus()
    {
        return isSprinting;
    }

    void SpringHandler()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Time.timeScale != 0)
        {
            if(!crouching)
            speed += 1;
            if (speed > 10)
                speed = 10;
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && Time.timeScale != 0)
        {
            if(!crouching)
            speed = 6;
            isSprinting = false;
        }
    }

    void CrouchHandler()
    {
        if (Input.GetKeyDown(KeyCode.C) && Time.timeScale != 0)
        {
            crouching = !crouching;
            if (crouching)
                controller.height = 1;
            else
                controller.height = 2;
        }
    }


}                                           