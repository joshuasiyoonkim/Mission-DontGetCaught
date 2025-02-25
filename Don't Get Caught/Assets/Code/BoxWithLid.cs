using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWithLid : MonoBehaviour
{
    public GameObject lid;
    public Transform player;
    public float detectionDistance = 3f;
    public bool isOpened = false;


    void Start()
    {

    }

    void Update()
    {
        if (player == null || isOpened) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionDistance)
        {


            if (Input.GetKeyDown(KeyCode.O))
            {
                OpenBox();
            }
        }

    }

    void OpenBox()
    {
        if (lid != null)
        {
            lid.SetActive(false);
        }



        isOpened = true;
    }

}
