using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Item currentlyHeldWeapon;
    public Transform weaponAnchor;
    public InventoryManager inventoryManager;

    [HideInInspector] GameObject weaponBeingReplaced;
    [HideInInspector] public GameObject currentTarget;
    void Start()
    {
        // Place the currently equipped item in character's hand
        HoldWeapon(currentlyHeldWeapon);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Hit();
        }
    }
    public void Hit()
    {
        if(currentTarget == null)
        {
            return;
        }
        BreakableObject breakableObject = currentTarget.GetComponent<BreakableObject>();
        int newHealthValue = breakableObject.health - currentlyHeldWeapon.attackPower;
        if(newHealthValue <= 0)
        {
            // Break or kill
            breakableObject.OutOfHealth();
        }
        else
        {
            breakableObject.TakeHit(newHealthValue);
            //Debug.Log("Target health: " + breakableObject.health + " / " + breakableObject.maxHealth);
            Debug.Log("Target health: " + newHealthValue + " / " + breakableObject.maxHealth);
            
        }
    }
    public void HoldWeapon(Item item)
    {
        Destroy(weaponBeingReplaced);
        if(item != null)
        {
            GameObject obj = Instantiate(item.itemPrefab, transform.position, transform.rotation);
            obj.transform.SetParent(weaponAnchor);
            weaponBeingReplaced = obj;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision entered by " + collision.gameObject.name);
        GameObject other = collision.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" || other.tag == "BreakableObject")
        {
            Debug.Log("Trigger hit by " + other.name);
            currentTarget = other.gameObject;
        }
        else if(other.tag == "DroppedItem")
        {
            Debug.Log("Other trigger hit by " + other.name);
            inventoryManager.AddItem(other.GetComponent<DroppedItem>().itemToAdd);
            Destroy(other.gameObject);
        }
    }
}
