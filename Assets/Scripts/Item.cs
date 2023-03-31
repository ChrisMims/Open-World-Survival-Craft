using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public TileBase tile;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;

    [Header("Weapons and Tools")]
    public int maxDurability;
    public int durability;
    public int attackPower;

    [Header("3D Model")]
    public GameObject itemPrefab;
}

public enum ItemType
{
    BuildingBlock,
    Tool
}
public enum ActionType
{
    Dig,
    Mine,
    Chop
}