using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;
    public Image countBackgroundImage;

    [HideInInspector] public Item item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;

    [Tooltip("Is the canvas a child of another game object?")] public bool canvasIsChild = false;
/*
    private void Start()
    {
        // Just for testing.
        InitialiseItem(item);
    }
*/
    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        //count = Random.Range(1, 12);
        RefreshCount();
    }
    public void RefreshCount()
    {
        // Controls the quantity count display on item icons
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
        countBackgroundImage.gameObject.SetActive(textActive);
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
