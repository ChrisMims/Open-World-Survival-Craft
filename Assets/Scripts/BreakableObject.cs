using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
public class BreakableObject : MonoBehaviour
{
    public int maxHealth, health;
    [Button("Restore Max Health")]
    private void DefaultSizedButton()
    {
        health = maxHealth;
    }
    [Required]
    public Item itemToDrop;
    public int minQuantity, maxQuantity;
    private int droppedQuantity;
    private Camera mainCamera;

    public DroppedItem droppedItem;

    [Required]
    [SerializeField] private MMFeedbacks MMFeedbacks;
    [Required]
    public MMProgressBar healthBar;

    void Start()
    {
        health = maxHealth;
        mainCamera = Camera.main;
        healthBar.MaximumBarFillValue = maxHealth;
        healthBar.MinimumBarFillValue = 0;
    }
    //}
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
        healthBar.UpdateBar(health, 0f, (100 * maxHealth));

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
