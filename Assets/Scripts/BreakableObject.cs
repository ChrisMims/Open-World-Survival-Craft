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

    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
