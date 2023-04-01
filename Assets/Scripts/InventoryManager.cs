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
    public static InventoryManager instance;
    
    [Tooltip("Starting items can be configured here.")]public Item[] startItems;

    public int maxStackedItems = 999;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab, mainInventoryWindow, openInventoryButton;
    [HideInInspector] public bool mainInventoryOpen = false;
    public Weapon weapon;

    int selectedSlot = -1;
    public InventoryDemo inventoryDemo;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        foreach(var item in startItems)
        {
            AddItem(item);
        }
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            // Toggle main inventory when the I key is pressed
            mainInventoryOpen = !mainInventoryOpen;
            ToggleInventory(mainInventoryOpen);
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

        inventoryDemo.GetSelectedItem();
        if (inventoryDemo.item != null)
        {
            Debug.Log("Received item: " + inventoryDemo.item);
        }
        weapon.HoldWeapon(inventoryDemo.item);
    }
    void ToggleInventory(bool mainInventoryVisible)
    {
        if (mainInventoryVisible)
        {
            mainInventoryWindow.SetActive(true);
            openInventoryButton.SetActive(false);
        }
        else
        {
            mainInventoryWindow.SetActive(false);
            openInventoryButton.SetActive(true);
        }
    }
    public bool AddItem(Item item)
    {
        // Check if any slot has the same item with count lower than maximum.
        // for (int i = 0; i < inventorySlots.Length; i++)
        // This method is supposedly less error-prone: https://stackoverflow.com/a/20940980
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
    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use)
            {
                itemInSlot.count--;
                if(itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject);
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }
        return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DroppedItem")
        {
            AddItem(other.GetComponent<DroppedItem>().itemToAdd);
            Destroy(other.gameObject);
        }
    }
}
