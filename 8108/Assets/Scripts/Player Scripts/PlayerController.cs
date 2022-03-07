using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    CapsuleCollider coll;
    Camera _camera;

    [Header("Movement")]
    public float speed;
    public float ClimbSpeed = 1f;
    public float walkingSpeed = 2f;
    public float runningSpeed = 5f; //  speed * 2
    public float crouchSpeed = 1f; //  speed / 2
    float movementMultiplier = 10f;
    public float rbDrag = 6f;
    [SerializeField] float acceleration = 10f;

    [Header("Crouch")]
    public float playerHeight = 1.82f;
    public float crouchHeight = 1;
    Vector3 velocity;

    //Input
    float horizontalMove;
    float verticalMove;
    Vector3 moveDirection;
    Rigidbody rb;

    [Header("Climbing")]
    public bool isClimbing;
    public LayerMask wallMask;
    Vector3 wallPoint;
    Vector3 wallNormal;
    float wallCount = 4f;

    [Header("Stats")]
    public float maxHealth = 100;
    float currentHealth;
    public float maxStamina = 20;
    float currentStamina;
    public HealthBar healthBar;

    [SerializeField] Transform groundPos;
    bool isMoving;
    



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        coll = GetComponent<CapsuleCollider>();
        _camera = Camera.main;
        currentHealth = maxHealth;
        currentStamina = maxStamina;


        healthBar.setMaxHealth(maxHealth);
    }

    void Update()
    {

        if (NearWall())
        {
            if (FacingWall() && currentStamina > 0)
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
            currentStamina = Mathf.Clamp(currentStamina - 1 * Time.deltaTime, 0, maxStamina);

        }
        else
        {
            MyInput();
            ControlDrag();
            ControlSpeed();

            currentStamina = Mathf.Clamp(currentStamina + 1 * Time.deltaTime, 0, maxStamina);

        }
        if (currentStamina <= 0)
        {
            isClimbing = !isClimbing;
        }

        var nearestGameObject = GetNearestGameObject();
        if (nearestGameObject == null) return;
        if (Input.GetButtonDown("Fire1"))
        {
            var interactable = nearestGameObject.GetComponent<IInteractable>();
            //Debug.Log(nearestGameObject.name);
            if (interactable == null) return;

            interactable.Interact();
        }

        

       /* if(isGrounded())
        {
            FindObjectOfType<SoundManager>().Play("Player Land");
            rbDrag = 6f;
        }
        else
        {
            rbDrag = 0f;
        }*/

    }
    void MyInput()
    {
        rb.useGravity = true;
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward * verticalMove + transform.right * horizontalMove;

        if (rb.velocity.magnitude >= 1)
            isMoving = true;
        else
            isMoving = false;



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

        if (isMoving)//&& !isGrounded())
            if (!SoundManager.Instance.isPlaying("Player Step"))
            {
                FindObjectOfType<SoundManager>().Play("Player Step");
                
            }
                
        //else
           // FindObjectOfType<SoundManager>().Stop("Player Step");
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
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthBar.SetHealth(currentHealth);
        Debug.Log("Health: " + currentHealth.ToString());
        if (currentHealth <= 0)
            Captured();
    }

    void Captured()
    {
        Destroy(this.gameObject);
        GameManager.Instance.YouLose();
    }

    private GameObject GetNearestGameObject()
    {
        GameObject result = null;
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 3))
        {
            result = hit.transform.gameObject;
        }
        return result;
    }

    /*bool isGrounded()
    {

       // RaycastHit hit = Physics.BoxCast(coll.bounds.center, coll.bounds.size, Vector3.down, Quaternion.identity, 1f, LayerMask.NameToLayer("Ground"));
        Color rayColor;

        if (hit.collider != null)
        {
            rayColor = Color.red;
        }
        else
        {
            rayColor = Color.green;
        }

        Debug.DrawRay(coll.bounds.center, Vector3.down * (coll.bounds.extents.y), rayColor);
        return hit.collider != null;
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Win Door"))
        {
            GameManager.Instance.YouWin();
        }
    }


}
