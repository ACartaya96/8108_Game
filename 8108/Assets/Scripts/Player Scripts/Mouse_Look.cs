using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Look : MonoBehaviour
{
    public float mouseSensitibity = 100f;      // in video it was set too 100f
    public Transform LookTarget;
    public Transform playerBody;                //should be First Person Player
    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        transform.LookAt(LookTarget);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitibity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitibity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //so he doesnt not look behind himself

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
