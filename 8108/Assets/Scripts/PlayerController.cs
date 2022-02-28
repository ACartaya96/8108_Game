using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public CapsuleCollider capsuleCollider;

    //move speed
    float speed;
    public float walkingSpeed = 5f;
    public float runningSpeed = 8f; //  speed * 2
    public float crouchSpeed =  2.5f ; //  speed / 2

    //falling variables
    public float gravity = -9.81f; //default was -9.81  set it -19.62 was too floatty

    //Crouching
    public float playerHeight =1.82f;
    public float crouchHeight = 1;
    Vector3 velocity;

    void Update()
    {
        
        //Getting Player Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Setting Player Movement
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        

       
        //Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runningSpeed;
            
        }
        else 
        {
            speed = walkingSpeed;
        }

        //Crouching
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C))
        {

            controller.height = crouchHeight; // hight divided by 2
            capsuleCollider.height = crouchHeight;
            speed = crouchSpeed;
        }
        else 
        {
            controller.height = playerHeight; // Character Controller Height was 3.8
            capsuleCollider.height = playerHeight;
        }

        //Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);  // (1/2) * g * T^2
    }
}
