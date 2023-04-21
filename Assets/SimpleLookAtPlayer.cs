using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class SimpleLookAtPlayer : MonoBehaviour
{
    public Transform transform;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        //transform = this.GetComponent<Transform>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(cam.transform);
        transform.LookAt(2 * transform.position - cam.transform.position);
    }
}
