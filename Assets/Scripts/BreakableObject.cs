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

    public TextMeshProUGUI healthText;
    public DroppedItem droppedItem;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

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
