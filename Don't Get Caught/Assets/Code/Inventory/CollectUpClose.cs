using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CollectUpClose : MonoBehaviour
{
    public string itemName;
    public float distance = 0.5f;
    public Transform player;
    public bool isInRange = false;
    public GameObject pickupUI;
    public TextMeshProUGUI pickupText;

    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    private void Update()
    {
        Mouse mouseInput = Mouse.current;

        float distanceToObject = Vector3.Distance(transform.position, player.position);

        if (distanceToObject <= distance)
        {
            if(!DialogueManager.isDialogueFinished)
            {
                return;
            }
            isInRange = true;
            ShowPickupUI();
        }
        else
        {
            isInRange = false;
            HidePickupUI();
        }



        //do this after user presses k
        Vector2 mousePosition = mouseInput.position.ReadValue();

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            if(!DialogueManager.isDialogueFinished)
            {
                return;
            }
            if (hit.collider.CompareTag("Collectable") && hit.collider.gameObject == this.gameObject && Input.GetKeyDown(KeyCode.E))
            {
                CollectItem();
            }
        }
    }

    private void ShowPickupUI()
    {
        if(itemName == null) {
            itemName = "item";
        }
        pickupText.text = $"Press 'E' to collect {itemName}?";
        pickupUI.SetActive(true);
    }

    private void HidePickupUI()
    {
        pickupUI.SetActive(false);
    }


    public void CollectItem()
    {
        if (playerInventory != null)
        {
            playerInventory.AddItem(itemName);
            playerInventory.UpdateInventoryUI();
        }

        Debug.Log($"{itemName} collected!");
        HidePickupUI();
        gameObject.SetActive(false);
    }
}
