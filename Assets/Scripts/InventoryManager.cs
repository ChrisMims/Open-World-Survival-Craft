using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{

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
}
