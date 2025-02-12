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
        interactionText.gameObject.SetActive(false); // Hide the text initially
        keyInside.SetActive(false); // Hide the key initially
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

        float elapsedTime = 0f;
        float duration = 1f;
        Quaternion startRotation = lid.localRotation;
        Quaternion endRotation = Quaternion.Euler(-90f, 0f, 0f); // Open lid

        while (elapsedTime < duration)
        {
            lid.localRotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        lid.localRotation = endRotation;
        keyInside.SetActive(true); // Reveal the key
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
            isPlayerNearby = false;
            interactionText.gameObject.SetActive(false);
        }
    }
}