using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Follow;
    public Vector3 offset;
    public float smoothSpeed = 0.1f; // You can adjust this value for smoother movement

    void LateUpdate()
    {
        Vector3 targetPosition = Follow.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }
}