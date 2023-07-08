using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    private bool doorIsOpen;
    private Animator animator;

    public Inventory inventory;
    public Item key;
    public Messager messager;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private void ChangeDoorState()
    {
        if (!doorIsOpen)
        {
            OpenDoor();
            doorIsOpen = true;
        }
        else
        {
            CloseDoor();
            doorIsOpen = false;
        }
    }

    private void OpenDoor()
    {
        animator.SetBool("Open", true);
    }

    private void CloseDoor()
    {
        animator.SetBool("Open", false);
    }
}
