using UnityEngine;

public class InventoryDemo : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickup;

    [HideInInspector] public Item item;

    public void PickupItem(int id)
    {
        bool result = inventoryManager.AddItem(itemsToPickup[id]);
/*        if (result)
        {
            Debug.Log("Item added to inventory.");
        }
        else
        {
            Debug.Log("Item not added. Inventory is full!");
        }*/
    }
    public void GetSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(false);
        item = receivedItem;
    }
    public void UseSelectedItem()
    {
        Item receivedItem = inventoryManager.GetSelectedItem(true);
        item = receivedItem;
    }
}
