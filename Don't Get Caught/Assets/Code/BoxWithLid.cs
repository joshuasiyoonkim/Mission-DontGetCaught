using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWithLid : MonoBehaviour
{
    public GameObject lid;
    public Transform player;
    public float detectionDistance = 2f;
    public bool isOpened = false;
    public GameObject text;
    bool textVisible = false;


    void Start()
    {
        if(text != null)
        {
            text.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null || isOpened || !DialogueManager.isDialogueFinished) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionDistance)
        {
            if(!textVisible)
            {
                text.SetActive(true);
                textVisible = true;
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                OpenBox();
            }
        } else {
            if(textVisible)
            {
                text.SetActive(false);
                textVisible = false;
            }
        }

    }

    void OpenBox()
    {
        if (lid != null)
        {
            lid.SetActive(false);
        }

        text.SetActive(false);

        isOpened = true;
    }

}
