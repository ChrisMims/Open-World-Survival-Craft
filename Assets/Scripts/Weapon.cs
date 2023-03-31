using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Item currentlyHeldWeapon;
    public MeshCollider meshCollider;
    public Transform weaponAnchor;

    [HideInInspector] GameObject weaponBeingReplaced;

    // Start is called before the first frame update
    void Start()
    {
        // Place the currently equipped item in character's hand
        HoldWeapon(currentlyHeldWeapon);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HoldWeapon(Item item)
    {
        Destroy(weaponBeingReplaced);
        GameObject obj = Instantiate(item.itemPrefab, transform.position, transform.rotation);
        obj.transform.SetParent(weaponAnchor);
        weaponBeingReplaced = obj;
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision entered");
        GameObject other = collision.gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
        {
            Debug.Log("Trigger hit by " + other.name);
        }

    }
}
