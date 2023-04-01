using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugManager : MonoBehaviour
{
    public TextMeshProUGUI attackPowerText;
    public Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(weapon != null && attackPowerText != null)
        {
            attackPowerText.text = weapon.currentlyHeldWeapon.attackPower.ToString();
        }
        else
        {
            //Debug.LogError("Null");
        }
    }
}
