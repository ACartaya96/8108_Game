using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    CapsuleCollider coll;

<<<<<<< Updated upstream
    //move speed
=======
    [Header("Movement")]
>>>>>>> Stashed changes
    public float speed;
    public float ClimbSpeed = 1f;
    public float walkingSpeed = 2f;
    public float runningSpeed = 5f; //  speed * 2
    public float crouchSpeed = 1f; //  speed / 2
    float movementMultiplier = 10f;
<<<<<<< Updated upstream
    float rbDrag = 6f;
    [SerializeField] float acceleration = 10f;

    //Crouching
    public float playerHeight = 1.82f;
    public float crouchHeight = 1;
    Vector3 velocity;
    
=======
    public float rbDrag = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Crouch")]
    public float playerHeight = 1.82f;
    public float crouchHeight = 1;
    Vector3 velocity;

>>>>>>> Stashed changes
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

<<<<<<< Updated upstream
    //healthBar
    public float maxHealth = 100;
    public float currentHealth;
    public HealthBar healthBar;


=======
    [Header("Stats")]
    public float maxHealth = 100;
    float currentHealth;
    public float maxStamina = 20;
    float currentStamina;
    public HealthBar healthBar;


>>>>>>> Stashed changes
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        coll = GetComponent<CapsuleCollider>();
        currentHealth = maxHealth;
<<<<<<< Updated upstream
=======
        currentStamina = maxStamina;
        

>>>>>>> Stashed changes
        healthBar.setMaxHealth(maxHealth);
    }

    void Update()
    {

        if (NearWall())
        {
<<<<<<< Updated upstream
            if (FacingWall())
=======
            if (FacingWall() && currentStamina > 0)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

            ClimbWall();
        }
        else 
        {
            MyInput();
            ControlDrag();
            ControlSpeed();
        }
       
       
=======

            ClimbWall();
            currentStamina = Mathf.Clamp(currentStamina - 1 *Time.deltaTime, 0, maxStamina);

        }
        else 
        {
            MyInput();
            ControlDrag();
            ControlSpeed();

            currentStamina = Mathf.Clamp(currentStamina + 1 * Time.deltaTime, 0, maxStamina);

        }
        if(currentStamina <= 0)
        {
            isClimbing = !isClimbing;
        }
 
        Debug.Log(currentStamina.ToString());
       
>>>>>>> Stashed changes
    }
    void MyInput()
    {
        rb.useGravity = true;
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward * verticalMove + transform.right * horizontalMove;
<<<<<<< Updated upstream

        
    }
    void ControlDrag()
=======
   

        
    }
   void ControlDrag()
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
        
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
        {
            wallCount--;
        }
        if (wallCount == 0.0f)
        {
=======
        {
            wallCount--;
        }
        if (wallCount == 0.0f)
        {
>>>>>>> Stashed changes
            isClimbing = false;
            wallCount = 4f;
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

<<<<<<< Updated upstream
}
=======
}
>>>>>>> Stashed changes
