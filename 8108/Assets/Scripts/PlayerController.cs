using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    CapsuleCollider coll;

    //move speed
    public float speed;
    public float ClimbSpeed = 1f;
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

    //Input
    float horizontalMove;
    float verticalMove;
    Vector3 moveDirection;
    Rigidbody rb;

    //climbing
    bool isClimbing;
    public LayerMask wallMask;
    Vector3 wallPoint;
    Vector3 wallNormal;
    float wallCount = 4f;

    //healthBar
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        coll = GetComponent<CapsuleCollider>();
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    void Update()
    {

        if (NearWall())
        {
            if (FacingWall())
            {
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    isClimbing = !isClimbing;
                }
            }
        }
        else 
        {
            isClimbing = false;
        }
        if (isClimbing)
        {
            speed = ClimbSpeed;

            ClimbWall();
        }
        else 
        {
            MyInput();
            ControlDrag();
            ControlSpeed();
        }
       
       
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
    bool NearWall()
    {
        return Physics.CheckSphere(transform.position, 3f, wallMask);
    }
    bool FacingWall()
    {
        RaycastHit hit;
        bool facingWall = Physics.Raycast(transform.position, transform.forward, out hit, coll.radius + 1f, wallMask);
        wallPoint = hit.point;
        wallNormal = hit.normal;
        return facingWall;
    }
    void ClimbWall()
    {
        rb.useGravity = false;

        GrabWall();

        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        moveDirection = transform.up * verticalMove + transform.right * horizontalMove;

       
    }

    void GrabWall()
    {
        Vector3 newPosition = wallPoint + wallNormal * (coll.radius - 0.1f);
        transform.position = Vector3.Lerp(transform.position,newPosition, 10 * Time.deltaTime);

        if (wallNormal == Vector3.zero)
            return;

        transform.rotation = Quaternion.LookRotation(-wallNormal);
    }
    void StopClimbBar()
    {
        while (wallCount >= 0.0f)
        {
            wallCount--;
        }
        if (wallCount == 0.0f)
        {
            isClimbing = false;
            wallCount = 4f;
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

}