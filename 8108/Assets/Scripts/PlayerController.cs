using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CapsuleCollider coll;

    //move speed
    public float speed;
    public float walkingSpeed = 2f;
    public float runningSpeed = 5f; //  speed * 2
    public float crouchSpeed = 1f; //  speed / 2
    float movementMultiplier = 10f;
    float rbDrag = 6f;
    [SerializeField] float acceleration = 10f;


    //Crouching
    public float playerHeight = 1.82f;
    public float crouchHeight = 1;
    Vector3 velocity;

    
    float horizontalMove;
    float verticalMove;
    Vector3 moveDirection;
    Rigidbody rb;

  

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        coll = GetComponent<CapsuleCollider>();

    }

    void Update()
    {
        MyInput();
        ControlDrag();
        ControlSpeed();
       
    }
    void MyInput()
    {
        rb.useGravity = true;
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMove + transform.right * horizontalMove;

        
    }
    void ControlDrag()
    {
        rb.drag = rbDrag;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * speed * movementMultiplier, ForceMode.Acceleration);
    }
    void ControlSpeed()
    {
        //Running
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = Mathf.Lerp(speed,runningSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            speed = Mathf.Lerp(speed, walkingSpeed, acceleration * Time.deltaTime);
        }

        //Crouching
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C))
        {
            coll.height = crouchHeight;
            speed = crouchSpeed;
        }
        else
        {
            coll.height = playerHeight;
        }
    }
    
}