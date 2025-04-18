using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI inventoryUI;

    void Start()
    {
        // Make sure inventoryUI is set for PlayerInventory when the scene loads
        if (inventoryUI != null)
        {
            PlayerInventory.instance.SetInventoryUI(inventoryUI);
        }
        else
        {
            Debug.LogWarning("Inventory UI is not assigned in the scene!");
        }
    }
}
