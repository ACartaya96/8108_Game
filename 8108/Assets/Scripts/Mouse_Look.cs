using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Look : MonoBehaviour
{
     
    public Transform LookTarget;

    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    float multiplier = 0.01f;

    Camera cam;
    float mouseX;
    float mouseY;
    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        //cam.transform.LookAt(LookTarget);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
   
        
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
    }
    void MyInput()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        yRotation += mouseX * sensX * multiplier ;
        xRotation -= mouseY * sensY * multiplier ;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //so he doesnt not look behind himself
    }
}
