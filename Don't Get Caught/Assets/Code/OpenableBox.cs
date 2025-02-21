using System.Collections;
using UnityEngine;
using TMPro; // Required for TextMeshPro support

public class OpenableBox : MonoBehaviour
{
    public Transform lid; // Assign the lid in Unity Inspector
    public TextMeshProUGUI interactionText; // UI text for "Press 'O' to Open"

    private bool isOpen = false;
    private bool isPlayerNearby = false;
    private Quaternion lidOpenRotation;

    private void Start()
    {
        if (interactionText == null) Debug.LogError("interactionText is NULL! Assign it in the Inspector.");
        if (lid == null) Debug.LogError("lid is NULL! Assign it in the Inspector.");

        interactionText.gameObject.SetActive(false); // Hide the text initially

        // Set the target rotation for the lid when it opens
        lidOpenRotation = Quaternion.Euler(lid.localEulerAngles.x - 90, lid.localEulerAngles.y, lid.localEulerAngles.z);
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.O) && !isOpen)
        {
            StartCoroutine(OpenBox());
        }
    }

    private IEnumerator OpenBox()
    {
        isOpen = true;
        interactionText.gameObject.SetActive(false); // Hide text when opening

        // Smoothly rotate the lid to the open position
        float elapsedTime = 0f;
        float duration = 1f; // Duration of the opening animation
        Quaternion initialRotation = lid.localRotation;

        while (elapsedTime < duration)
        {
            lid.localRotation = Quaternion.Slerp(initialRotation, lidOpenRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lid.localRotation = lidOpenRotation; // Ensure the lid is exactly in the open position
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