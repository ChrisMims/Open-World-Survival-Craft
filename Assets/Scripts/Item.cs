using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Scriptable Object/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]

    [HorizontalGroup("Split", 55, LabelWidth = 70)]
    [HideLabel, PreviewField(55, ObjectFieldAlignment.Left)]
    public TileBase tile;
    [VerticalGroup("Split/Meta")]
    public ItemType type;
    [VerticalGroup("Split/Meta")]
    public ActionType actionType;
    [VerticalGroup("Split/Meta")]
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