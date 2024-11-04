using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private Transform cam;

    private Vector3 offset;
    void Start()
    {
        cam = Camera.main.transform;
        offset = cam.position - target.position;
    }
    
    void Update()
    {
        cam.position = target.position + offset;
    }
}
