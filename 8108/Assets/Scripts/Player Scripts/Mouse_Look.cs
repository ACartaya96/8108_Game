using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Look : MonoBehaviour
{
    public float mouseSensitibity = 100f;      // in video it was set too 100f
    public GameObject player;                //should be First Person Player
    public Transform playerBody;
    float xRotation = 0f;
    public float camRotateSpeed =-0.5f;
    private Vector3 rotateValue;  

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitibity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitibity * Time.deltaTime;

        if(GameManager.Instance.GameIsPaused == false)
            Cursor.lockState = CursorLockMode.Locked;

        xRotation = Mathf.Clamp(xRotation - mouseY, -90f, 90f); //so he doesn't not look behind himself

        transform.localEulerAngles = new Vector3(xRotation, 0f, 0f);
        player.transform.Rotate(Vector3.up * mouseX);
        if(player.GetComponent<PlayerController>().isClimbing)
        {
            transform.rotation *= Quaternion.Euler(0, 180, 0);
        } 
        
    }
}
