using UnityEngine;

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
    */

    public int maxStackedItems = 999;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;
    private void Start()
    {
        ChangeSelectedSlot(0);
    }
    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 9)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }
    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }
    public bool AddItem(Item item)
    {
        // Check if any slot has the same item with count lower than maximum.
        //for (int i = 0; i < inventorySlots.Length; i++)
        for (int i = inventorySlots.GetLowerBound(0); i <= inventorySlots.GetUpperBound(0); i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.count < maxStackedItems &&
                itemInSlot.item.stackable == true)
            {
                // If a stack < 999, add the new item to the same slot
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }
        // Find an empty slot
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
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
