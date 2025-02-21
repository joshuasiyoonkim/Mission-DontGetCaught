using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class WrongDoor : MonoBehaviour
{
    public bool open;
    public float smooth = 1.0f;
    float DoorOpenAngle = -90.0f;
    float DoorCloseAngle = 0.0f;
    public AudioSource asource;
    public AudioClip openDoor, closeDoor;
    public GameObject interactionText;
    private bool isPlayerNearby = false;
    // Use this for initialization
    void Start()
    {
        asource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isPlayerNearby)
        {
            if (!open && interactionText != null)
            {
                interactionText.SetActive(true);
                interactionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Press 'E' to open the door";

            }


        }
    }

    public void OpenDoor()
    {
        open = !open;
        asource.clip = open ? openDoor : closeDoor;
        asource.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            if (!open && interactionText != null)
            {
                interactionText.SetActive(true);
                interactionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Press 'E' to Open";
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            if (interactionText != null)
            {
                interactionText.SetActive(false);
            }
        }
    }

    private void ShowMessage(string message)
    {
        if (interactionText != null)
        {
            interactionText.SetActive(true);
            interactionText.GetComponent<TMPro.TextMeshProUGUI>().text = message;
            StartCoroutine(HideMessageAfterDelay());
        }
    }

    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(2);
        interactionText.SetActive(false);
    }
}
