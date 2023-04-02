using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public GameObject playerCharacter;
    public InventoryManager playerInventory;
    public InventoryManager chestInventory;

    public PlayerPrefs playerPrefs;
    // Start is called before the first frame update
    void Start()
    {
        //playerCharacter.transform.position

    }

    // Loop through each inventory and write the contents of their arrays to prefs?
    public void SaveInventory()
    {

    }
    public void LoadInventory()
    {

    }
}
