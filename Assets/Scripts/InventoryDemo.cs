using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDemo : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
        if (result)
        {
            Debug.Log("Item added to inventory.");
        } else
        {
            Debug.Log("Item not added. Inventory is full!");
        }
    }
}
