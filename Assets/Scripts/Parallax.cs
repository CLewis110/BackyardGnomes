using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier;
    private Transform camTransform;
    private Vector3 lastCameraPosition;

    private void Start()
    {
        camTransform = Camera.main.transform;
        lastCameraPosition = camTransform.position;
    }

    private void Update()
    {
        Vector3 deltaMovement = camTransform.position - lastCameraPosition;
        transform.position += deltaMovement * parallaxMultiplier;
        lastCameraPosition = camTransform.position;
    }
}
