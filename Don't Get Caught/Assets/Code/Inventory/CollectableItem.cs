using UnityEngine;
using TMPro;

public class CollectibleItem : MonoBehaviour
{
    public string itemName = "Key";
    public GameObject pickupUI;
    public TextMeshProUGUI pickupText;

    private PlayerInventory playerInventory;
    public bool isLookedAt = false;

    private void Start()
    {
        pickupUI.SetActive(false);
        playerInventory = FindObjectOfType<PlayerInventory>();
    }

    private void Update()
    {
        if (isLookedAt)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                ShowPickupUI();
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
        else
        {
            HidePickupUI();
        }
    }

    public void ShowPickupUI()
    {
        pickupText.text = $"Do you want to collect the {itemName}?";
        pickupUI.SetActive(true);
    }

    public void HidePickupUI()
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
        Destroy(gameObject, 0.1f);
    }

    public void CancelPickup()
    {
        HidePickupUI();
    }
}
