using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator animator;

    //public GameObject requiredSender;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact(GameObject sender = null)
    {
        //bool shouldOpen = false;

        //if(!requiredSender)
        //{
        //    shouldOpen = true;
        //} else if(requiredSender == sender)
        //{
        //    shouldOpen = true;
        //}

        //if(shouldOpen)
        //{
        //    animator.SetTrigger("Open");
        //}
        print("opening door");

        animator.SetTrigger("Open");
    }
}
