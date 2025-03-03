using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]

public class Door_Home : MonoBehaviour
{
	public bool open;
	public bool isWrongDoor = false;
	public float smooth = 1.0f;
	float DoorOpenAngle = -90.0f;
	float DoorCloseAngle = 0.0f;
	public AudioSource asource;
	public AudioClip openDoor, closeDoor;
	public GameObject interactionText;
	private bool isPlayerNearby = false;


	void Start()
	{
		asource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (open)
		{
			var target = Quaternion.Euler(0, DoorOpenAngle, 0);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
		}
		else
		{
			var target1 = Quaternion.Euler(0, DoorCloseAngle, 0);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
		}

		if (isPlayerNearby)
		{
			if (!open && interactionText != null)
			{
				interactionText.SetActive(true);
				interactionText.GetComponent<TMPro.TextMeshProUGUI>().text = "Press 'E' to open the door";

				if (Input.GetKeyDown(KeyCode.E))
				{
					if (!isWrongDoor)


						OpenDoor();

				}
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
			isPlayerNearby = true;
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
