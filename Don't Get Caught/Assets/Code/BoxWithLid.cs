using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxWithLid : MonoBehaviour
{
    public GameObject lid;
    public GameObject interactionText;
    public Transform player;
    public float detectionDistance = 3f;
    public bool isOpened = false;


    void Start()
    {
        if (interactionText != null)
        {
            interactionText.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null || isOpened) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionDistance)
        {
            interactionText.SetActive(true);
            interactionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Press 'O' to open the box";

            if (Input.GetKeyDown(KeyCode.O))
            {
                OpenBox();
            }
        }
        else
        {
            interactionText.SetActive(false);
        }
    }

    void OpenBox()
    {
        if (lid != null)
        {
            lid.SetActive(false);
        }

        if (interactionText != null)
        {
            interactionText.SetActive(false);
        }

        isOpened = true;
    }

}
