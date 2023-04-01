using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRaycast : MonoBehaviour
{
    Ray ray;
    public float maxDistance = 50f;
    public float hitRate = 0.25f;
    public Transform rayStartLocation;

    private Camera cam;
    // Duration that the line is visible
    private WaitForSeconds hitDuration = new WaitForSeconds(0.7f);
    private LineRenderer lineRenderer;
    private float nextHit;
    public LayerMask layersToHit;

    // Start is called before the first frame update
    void Start()
    {
        ray = new Ray(transform.position, transform.forward);

        lineRenderer = GetComponent<LineRenderer>();
        cam = Camera.main;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;
            lineRenderer.SetPosition(0, rayStartLocation.position);

            if(Physics.Raycast(rayOrigin, cam.transform.forward, out hit, maxDistance, layersToHit))
            {
                lineRenderer.SetPosition(1, hit.point);
                Debug.Log(hit.collider.gameObject.name + " was hit! \n" + hit.collider.gameObject.layer);
            }
        }
    }
    private IEnumerator ShotEffect()
    {
        lineRenderer.enabled = true;
        yield return hitDuration;
        lineRenderer.enabled = false;
    }
}
