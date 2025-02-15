using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DoorScript
{
	[RequireComponent(typeof(AudioSource))]


	public class Door : MonoBehaviour
	{
		public bool open;
		public float smooth = 1.0f;
		float DoorOpenAngle = -90.0f;
		float DoorCloseAngle = 0.0f;
		public AudioSource asource;
		public AudioClip openDoor, closeDoor;
		public GameObject interactionText;
		private bool isPlayerNearby = false;
		private PlayerInventory playerInventory; // Reference to the PlayerInventory

		void Start()
		{
			asource = GetComponent<AudioSource>();
			if (interactionText != null)
			{
				interactionText.SetActive(false);
			}
			playerInventory = FindObjectOfType<PlayerInventory>(); // Find the PlayerInventory in the scene
		}

		// Update is called once per frame
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

			if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
			{

				if (playerInventory != null && playerInventory.HasItem("Key"))
				{
					OpenDoor();
				}
				else
				{
					ShowMessage("You need a key!");
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
}