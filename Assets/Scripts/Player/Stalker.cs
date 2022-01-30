using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : MonoBehaviour
{

    public Transform target;

    void Update()
    {

        transform.LookAt(target, Camera.main.transform.up);
    }
}
