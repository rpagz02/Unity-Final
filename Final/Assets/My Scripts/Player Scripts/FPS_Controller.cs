using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FPS_Controller : MonoBehaviour
{
    #region Local Variables
    [SerializeField]
    private float speed;
    private Camera m_Camera;
    private float jumpSpeed = 8.0F;
    private float gravity = 30.0F;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;
    #endregion Local Variables

    #region Variables accessed by other scripts
    public bool isMoving = false;
    private bool isSprinting = false;
    #endregion Variables accessed by other scripts



    void Start ()
    {
        speed = 4;
        Cursor.lockState = CursorLockMode.Locked;
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
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);



        
                                                                                                                                                                      
        if (Input.GetKey(KeyCode.LeftShift))                   
        {                                                       
            speed += 1;                                        
            if (speed > 7)                                    
                speed = 7;
            isSprinting = true;
        }                                                      
        if(Input.GetKeyUp(KeyCode.LeftShift))                  
        {                                                        
            speed = 4;
            isSprinting = false;
        }                                                      
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

}                                           