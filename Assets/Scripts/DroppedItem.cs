using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroppedItem : MonoBehaviour
{
    [HideInInspector] public Item itemToAdd;
    private Camera mainCamera;
    void Start()
    {
        // Makes sure that the UI element always faces the camera.
        mainCamera = Camera.main;

        //item = ;
    }
    void Update()
    {
        this.transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.back, mainCamera.transform.rotation * Vector3.up);
    }
    public void UpdateTexture(GameObject lastDropped, Item item)
    {
        lastDropped.GetComponentInChildren<Image>().sprite = item.image;
        itemToAdd = item;
        lastDropped.GetComponent<BoxCollider>().enabled = true;
    }
}
