using System.Collections;
using UnityEngine;
using TMPro; // Required for TextMeshPro support

public class OpenableBox : MonoBehaviour
{
    public Transform lid; // Assign the lid in Unity Inspector
    public GameObject keyInside; // Assign the hidden key inside the box
    public TextMeshProUGUI interactionText; // UI text for "Press 'O' to Open"

    private bool isOpen = false;
    private bool isPlayerNearby = false;

    private void Start()
    {
        if (interactionText == null) Debug.LogError("interactionText is NULL! Assign it in the Inspector.");
        if (keyInside == null) Debug.LogError("keyInside is NULL! Assign it in the Inspector.");
        if (lid == null) Debug.LogError("lid is NULL! Assign it in the Inspector.");

        interactionText.gameObject.SetActive(false); // Hide the text initially
        keyInside.SetActive(false); // Hide the key initially
    }

    private void Update()
    {
           if (isPlayerNearby)
           {
                Debug.Log("Player is near the box."); // Check if the player is in range
           }

           if (isPlayerNearby && Input.GetKeyDown(KeyCode.O) && !isOpen)
           {
               Debug.Log("O key pressed, opening box..."); // Check if the "O" key press is registered
               StartCoroutine(OpenBox());
           }


       if (isPlayerNearby && Input.GetKeyDown(KeyCode.O) && !isOpen)
       {
           StartCoroutine(OpenBox());
       }
    }

    private IEnumerator OpenBox()
    {
        isOpen = true;
        interactionText.gameObject.SetActive(false); // Hide text when opening

        yield return new WaitForSeconds(1f); // Simulate opening delay
        lid.gameObject.SetActive(false); // Make the lid disappear
        keyInside.SetActive(true); // Reveal the key
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone.");
            isPlayerNearby = true;
            if (!isOpen)
            {
                interactionText.text = "Press 'O' to Open";
                interactionText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left trigger zone.");
            isPlayerNearby = false;
            interactionText.gameObject.SetActive(false);
        }
    }

}
