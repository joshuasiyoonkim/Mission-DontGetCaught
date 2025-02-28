using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOpenBox : MonoBehaviour
{
    public float DistanceOpen = 3f; // Distance to detect the box
    public GameObject text; // UI text to show interaction prompt

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, DistanceOpen))
        {
            OpenableBox box = hit.transform.GetComponent<OpenableBox>();

            if (box != null)
            {
                Debug.Log("testing");
                text.SetActive(true);

                if (Input.GetKeyDown(KeyCode.O))
                {
                    //box.OpenBox();
                }
            }
            else
            {
                text.SetActive(false);
            }
        }
        else
        {
            text.SetActive(false);
        }
    }
}
