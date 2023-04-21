using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public Weapon weapon;
    public TextMeshProUGUI targetText;
    public WaitForSeconds tickLength = new WaitForSeconds(2f);
    private float nextTick;
    private void Start()
    {
        // Make sure this is cleared when starting the game.
        targetText.text = null;
    }
    public void UpdateTargetText(TextMeshProUGUI targetText, string newString)
    {
        if(targetText != null && newString != null)
        {
            targetText.text = newString;
            StartCoroutine(GlobalTick());
        }
        else
        {
            targetText.text = null;
        }
    }
    private IEnumerator GlobalTick()
    {
        yield return tickLength;
        targetText.text = null;
    }

}
