using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CollectibleItem : MonoBehaviour
{
    public string itemName = "Key";
    public GameObject pickupUI;
    public TextMeshProUGUI pickupText;
    public Transform player;
    public float detectionDistance = 3f;
    public BoxWithLid box;  // Reference to the box with the lid

    public bool isInRange = false;
    public bool isOpened = false;

    private void Start()
    {
        pickupUI.SetActive(false);


    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionDistance)
        {
            isInRange = true;

            // Only show interaction if the box is opened
            if (box != null && box.isOpened)
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    ShowPickupUI();
                }
            }
        }
        else
        {
            isInRange = false;
            HidePickupUI();
        }

        if (pickupUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                CollectItem();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                CancelPickup();
            }
        }
    }

    private void ShowPickupUI()
    {
        pickupText.text = $"Do you want to collect the {itemName}?";
        pickupUI.SetActive(true);
    }

    private void HidePickupUI()
    {
        pickupUI.SetActive(false);
    }

    public void CollectItem()
    {
        PlayerInventory.instance.AddItem(itemName);
        PlayerInventory.instance.UpdateInventoryUI();

        Debug.Log($"{itemName} collected!");
        HidePickupUI();
        gameObject.SetActive(false);
    }

    public void CancelPickup()
    {
        HidePickupUI();
    }
}