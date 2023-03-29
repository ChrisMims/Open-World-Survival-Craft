using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block// : MonoBehaviour
{
    //public int x, y, z;
    //public int blockID;
    public string blockName;
    public string blockDescription;
    public Image blockImage;
    public Texture blockTexture;
    
    public Block(string newName, string newDescription)
    {
        // Should I only pass the ID? No...
        blockName = newName;
        blockDescription = newDescription;
        blockImage = null;
        blockTexture = null;
    }
}
