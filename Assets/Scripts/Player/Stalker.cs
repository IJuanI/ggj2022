using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : MonoBehaviour
{

    public Transform target;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    void Update()
    {
        Vector3 forward = target.position - transform.position;
        forward.y = 0;
        transform.rotation = Quaternion.LookRotation(forward, rb.transform.up);
    }
}
