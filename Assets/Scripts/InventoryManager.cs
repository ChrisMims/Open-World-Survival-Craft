using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
/*
    // 001
    public int BlockID { get; set; }
    // Dirt
    public string BlockName { get; set; }
    // A block of dirt
    public string BlockDescription { get; set; }
    // 7
    public string BlockType { get; set; }

    List<Block> blockList = new List<Block>();

    // Start is called before the first frame update
    void Start()
    {
        // Create list/array of items and quantity
        //        blockList.Add(new Block());
        blockList.Add(new Block("Dirt", "A block of dirt..."));
        blockList.Add(new Block("Stone", "A large chunk of stone."));
        Debug.Log(blockList.Count);
        Debug.Log(blockList[1].blockName + "\n" + blockList[1].blockDescription);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
*/
    
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public bool AddItem(Item item)
    {
        // Find an empty slot
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if(itemInSlot == null)
            {
                // If an empty slot is found, spawn an item in it.
                SpawnNewItem(item, slot);
                return true;
            }
        }
        // If no free slot is found:
        return false;
    }
    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }
}
