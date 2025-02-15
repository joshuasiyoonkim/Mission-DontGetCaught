using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraOpenDoor : MonoBehaviour
{
	public float DistanceOpen = 3;
	public GameObject text;
	private PlayerInventory playerInventory;

	void Start()
	{
		playerInventory = FindObjectOfType<PlayerInventory>();

	}

	// Update is called once per frame
	void Update()
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, DistanceOpen))
		{
			if (hit.transform.GetComponent<DoorScript.Door>())
			{
				text.SetActive(true);
				if (Input.GetKeyDown(KeyCode.E))
				{
					if (playerInventory != null && playerInventory.HasItem("Key"))
					{
						hit.transform.GetComponent<DoorScript.Door>().OpenDoor();
						playerInventory.RemoveItem("Key");
					}
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

