using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(MeshFilter))]
public class Door : MonoBehaviour {

	public enum rotOrient
	{
		Y_Axis_Up,
		Z_Axis_Up
	}

	public rotOrient rotationOrientation;
	public float doorOpenAngle = 90.0f;
	public float doorClosedAngle = 0.0f;
	[Range(1,15)] public float speed = 10.0f;

	public AudioClip doorOpenSound;
	public AudioClip doorCloseSound;

	Quaternion doorOpen = Quaternion.identity;
	Quaternion doorClosed = Quaternion.identity;

	DialogueManager dialogueManager;

	bool doorStatus = false;
	[HideInInspector]
	public bool playerInRange = false;

	void Start() {
		dialogueManager = FindObjectOfType<DialogueManager>();
		if (this.gameObject.isStatic) {
			Debug.Log ("This door has been set to static and won't be openable. Doorscript has been removed.");
			Destroy (this);
		}
		if (rotationOrientation.Equals (rotOrient.Z_Axis_Up)) {
			doorOpen = Quaternion.Euler (transform.localEulerAngles.x, transform.localEulerAngles.y, doorOpenAngle);
			doorClosed = Quaternion.Euler (transform.localEulerAngles.x, transform.localEulerAngles.y, doorClosedAngle);
		} else {
			doorOpen = Quaternion.Euler (transform.localEulerAngles.x, doorOpenAngle, transform.localEulerAngles.z);
			doorClosed = Quaternion.Euler (transform.localEulerAngles.x, doorClosedAngle, transform.localEulerAngles.z);
		}
	}

	void Update() {

		if (playerInRange) {
			if (Input.GetKeyDown (KeyCode.E)) {
				//check what scene it is
				//if home scene, only require key
				//if maze scene, require 2 books
				string currentScene = SceneManager.GetActiveScene().name;

				if(currentScene.Equals("Home with full house"))
                {
					if (PlayerInventory.instance.items.ContainsKey("Key"))
					{
						//if its the kitchen door, play the dialogue and open the door
						if (this.CompareTag("KitchenDoor") && dialogueManager != null)
						{
							//beginning of the game
							if(!GameManager.instance.mazeCompleted && !GameManager.instance.outdoorCompleted)
							{
								dialogueManager.StartDialogueSequence(2);
							}
							//after they completed both of the scenes
							else if (GameManager.instance.mazeCompleted && GameManager.instance.outdoorCompleted)
                            {
								dialogueManager.StartDialogueSequence(4);
                            }
							//after they completed one of the scenes
							else
							{
								dialogueManager.StartDialogueSequence(3);
                            }

						}
						if (this.CompareTag("MasterBedroom") && dialogueManager != null)
                        {
							//if you have not completed both maze and outdoor play the dialogue
							if(GameManager.instance.mazeCompleted && GameManager.instance.outdoorCompleted)
                            {
								Debug.Log("completed both, dont play audio");
                            } else
                            {
								Debug.Log("starting dialogue");
								dialogueManager.StartDialogueSequence(5);
                            }
                        }
						if (doorStatus)
						{
							StartCoroutine(this.moveDoor(doorClosed));
							if (doorCloseSound != null) AudioSource.PlayClipAtPoint(doorCloseSound, this.transform.position);
						}
						else
						{
							StartCoroutine(this.moveDoor(doorOpen));
							if (doorOpenSound != null) AudioSource.PlayClipAtPoint(doorOpenSound, this.transform.position);
						}
					}
					else
					{
						if (doorOpenSound != null && dialogueManager != null)
						{
							AudioSource.PlayClipAtPoint(doorOpenSound, this.transform.position);
							Debug.Log("door is locked, get a key");
							dialogueManager.StartDialogueSequence(1);
						}
					}
				} else if(currentScene.Equals("MazeScene"))
                {
					int booksNeeded;
					if(GameManager.instance.outdoorCompleted)
                    {
						booksNeeded = 4;
					} else
                    {
						booksNeeded = 2;
                    }
					if (PlayerInventory.instance.items.TryGetValue("Book", out int count) && count == booksNeeded)
					{
						if (doorStatus)
						{
							StartCoroutine(this.moveDoor(doorClosed));
							if (doorCloseSound != null) AudioSource.PlayClipAtPoint(doorCloseSound, this.transform.position);
						}
						else
						{
							StartCoroutine(this.moveDoor(doorOpen));
							if (doorOpenSound != null) AudioSource.PlayClipAtPoint(doorOpenSound, this.transform.position);
						}
					}
					else
					{
						if (doorOpenSound != null && dialogueManager != null)
						{
							AudioSource.PlayClipAtPoint(doorOpenSound, this.transform.position);
							Debug.Log("you need 2 books to open this door");
							dialogueManager.StartDialogueSequence(1);
						}
					}
                }
				else if(currentScene.Equals("OutdoorScene"))
                {
					int booksNeeded;
					if (GameManager.instance.mazeCompleted)
					{
						booksNeeded = 4;
					}
					else
					{
						booksNeeded = 2;
					}
					if (PlayerInventory.instance.items.TryGetValue("Book", out int count) && count == booksNeeded)
					{
						if (doorStatus)
						{
							StartCoroutine(this.moveDoor(doorClosed));
							if (doorCloseSound != null) AudioSource.PlayClipAtPoint(doorCloseSound, this.transform.position);
						}
						else
						{
							StartCoroutine(this.moveDoor(doorOpen));
							if (doorOpenSound != null) AudioSource.PlayClipAtPoint(doorOpenSound, this.transform.position);
						}
					}
					else
					{
						if (doorOpenSound != null && dialogueManager != null)
						{
							AudioSource.PlayClipAtPoint(doorOpenSound, this.transform.position);
							Debug.Log("you need 2 books to open this door");
							dialogueManager.StartDialogueSequence(1);
						}
					}
                }
			}
		}

	}

	IEnumerator moveDoor(Quaternion target) {
		while (Quaternion.Angle (transform.localRotation, target) > 0.5f) {
			transform.localRotation = Quaternion.Slerp (transform.localRotation, target, Time.deltaTime * speed);
			yield return null;
		}
		doorStatus = !doorStatus;
		yield return null;
	}
}
