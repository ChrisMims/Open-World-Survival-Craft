using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BreakableObject : MonoBehaviour
{
    public int maxHealth, health;
    public Item itemToDrop;
    public int minQuantity, maxQuantity;
    private int droppedQuantity;
    private Camera mainCamera;

    public TextMeshProUGUI healthText;
    public DroppedItem droppedItem;
    void Start()
    {
        health = maxHealth;
        mainCamera = Camera.main;
    }
    private void Update()
    {
        // Makes sure that the UI element always faces the camera.
        this.transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
    /// <summary>
    /// Handles dropping items and calculating damage received when hit.
    /// </summary>
    /// <param name="amount"></param>
    public void TakeHit(int amount)
    {
        // Amount of damage to take
        health = amount;
        // Play sound

        // Play animation

        // Update healthbar
        healthText.text = ("[" + health.ToString() + " / " + maxHealth.ToString() + "] HP");
        // Randomly drop items <= maxQuantity
        if (droppedQuantity < maxQuantity)
        {
            var rndm = Random.Range(0f, minQuantity);

            if ((rndm + droppedQuantity) < maxQuantity)
            {
                Debug.Log("Dropping " + rndm + " of " + itemToDrop);
                for (int i = 0; i < rndm; i++)
                {
                    droppedQuantity++;

                    // Instantiate
                    GameObject lastDropped = Instantiate(itemToDrop.itemPrefab, transform.position, transform.rotation);
                    droppedItem.UpdateTexture(lastDropped,itemToDrop);
                }
            }
        }
    }

    /// <summary>
    /// Handles dropping items and EXP when object is destroyed.
    /// </summary>
    public void OutOfHealth()
    {
        // If dropped
        if(droppedQuantity < minQuantity)
        {
            // Drop the rest. Use a loop?
            for (int i = droppedQuantity; i < minQuantity; i++)
            {
                // Drop (instantiate) one more
                GameObject lastDropped = Instantiate(itemToDrop.itemPrefab, transform.position, transform.rotation);
                droppedItem.UpdateTexture(lastDropped, itemToDrop);
            }

        }

        Destroy(this.gameObject);
        Debug.LogWarning(this.name + " has died!");
    }
}
