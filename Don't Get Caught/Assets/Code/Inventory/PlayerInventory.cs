using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<string, int> items = new Dictionary<string, int>();
    public TextMeshProUGUI inventoryUI;

    public void AddItem(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName] += 1;
        }
        else
        {
            items[itemName] = 1;
        }

        Debug.Log($"{itemName} added to inventory. Quantity: {items[itemName]}");
        UpdateInventoryUI();

    }

    public bool HasItem(string itemName)
    {
        return items.ContainsKey(itemName);
    }

    public int GetItemQuantity(string itemName)
    {
        return items.ContainsKey(itemName) ? items[itemName] : 0;
    }

    public void PrintInventory()
    {
        foreach (var item in items)
        {
            Debug.Log($"{item.Key}: {item.Value}");
        }
    }


    public void RemoveItem(string itemName)
    {
        if (items.ContainsKey(itemName))
        {
            items[itemName] -= 1;
            if (items[itemName] <= 0)
            {
                items.Remove(itemName);
            }
        }
        UpdateInventoryUI();

    }

    public void UpdateInventoryUI()
    {
        if (inventoryUI != null)
        {
            inventoryUI.text = "Inventory:\n";

            foreach (var item in items)
            {
                inventoryUI.text += $"{item.Key}: {item.Value}\n";
            }
        }
    }
}
