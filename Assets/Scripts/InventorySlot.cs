using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [Required]
    [PreviewField]
    //[HorizontalGroup("Split", 55, LabelWidth = 70)]
    public Image image;
    //[VerticalGroup("Split/Right")]
    [ColorPalette]
    public Color selectedColor, unselectedColor;
    private void Awake()
    {
        Deselect();
    }
    public void Select()
    {
        image.color = selectedColor;
    }
    public void Deselect()
    {
        image.color = unselectedColor;
    }
    // Drag and drop
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }
}
