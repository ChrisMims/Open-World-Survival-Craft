using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;

    [HideInInspector] public Item item;
    [HideInInspector] public Transform parentAfterDrag;

    [Tooltip("Is the canvas a child of another game object?")] public bool canvasIsChild = false;

    private void Start()
    {
        // Just for testing.
        InitialiseItem(item);
    }
    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
    }
    // Drag and drop inventory items
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        parentAfterDrag = transform.parent;

        if (canvasIsChild)
        {
            transform.SetParent(transform.parent.parent);
        }
        else
        {
            transform.SetParent(transform.root);
        }
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }
}
