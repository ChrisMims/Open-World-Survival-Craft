using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using MoreMountains.Feedbacks;

public class Weapon : MonoBehaviour
{
    [HorizontalGroup("Split", 55, LabelWidth = 70)]
    [HideLabel, PreviewField(55, ObjectFieldAlignment.Left)]
    public Item currentlyHeldWeapon;
    [VerticalGroup("Split/Meta")]
    public Transform weaponAnchor;
    [VerticalGroup("Split/Meta")]
    public InventoryManager inventoryManager;
    [VerticalGroup("Split/Meta")]
    public HUDManager hudManager;
    
    [HideInInspector] public int currentDurability;
    [HideInInspector] GameObject weaponBeingReplaced;
    [HideInInspector] public GameObject currentTarget;

    Ray ray;
    [Range(5f, 100f)]
    [Tooltip("Defines the maximum range of a weapon's reach.")]
    public float maxDistance = 50f;
    public float hitRate = 0.25f;
    public Transform rayStartLocation;

    private Camera cam;
    // Duration that the line is visible
    private WaitForSeconds hitDuration = new WaitForSeconds(0.7f);
    private LineRenderer lineRenderer;
    private float nextHit;
    public LayerMask layersToHit;

    private DebugManager debugManager;
    [SerializeField] private MMFeedbacks MMFeedbacks;
    private void Awake()
    {
        // For debugging purposes:
        //debugManager = GameObject.Find("DebugManager").GetComponent<DebugManager>();
        Cursor.lockState = CursorLockMode.None;
    }
    void Start()
    {
        // Place the currently equipped item in character's hand
        HoldWeapon(currentlyHeldWeapon);

        ray = new Ray(transform.position, transform.forward);

        lineRenderer = GetComponent<LineRenderer>();
        cam = Camera.main;
    }    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(ShotEffect());
            Hit();
            //MMFeedbacks.PlayFeedbacks();
        }
    }
    public void Hit()
    {
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        lineRenderer.SetPosition(0, rayStartLocation.position);

        if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, maxDistance, layersToHit))
        {
            lineRenderer.SetPosition(1, hit.point);
            currentTarget = hit.transform.gameObject;
            if(currentTarget.tag != "Enemy" &&
                currentTarget.tag != "BreakableObject" &&
                currentTarget.tag != "UsableObject")
            {
                //Debug.LogError("Null");
                return; 
            }
            BreakableObject breakableObject = currentTarget.GetComponent<BreakableObject>();
            int newHealthValue = breakableObject.health - currentlyHeldWeapon.attackPower;
            if(newHealthValue <= 0) { breakableObject.OutOfHealth(); }
            else
            {
                hudManager.UpdateTargetText(hudManager.targetText, currentTarget.name);
                breakableObject.TakeHit(newHealthValue);
                Debug.Log("Target health: " + newHealthValue + " / " + breakableObject.maxHealth);
            }
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
            currentlyHeldWeapon = item;            
        }
    }
    private IEnumerator ShotEffect()
    {
        lineRenderer.enabled = true;
        yield return hitDuration;
        lineRenderer.enabled = false;
    }
}
