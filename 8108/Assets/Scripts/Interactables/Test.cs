using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour, IInteractable
{
    public GameObject test;
    void Testing()
    {
        Debug.Log("This is a Test");
    }

    public void Interact()
    {
        Testing();
    }
}
