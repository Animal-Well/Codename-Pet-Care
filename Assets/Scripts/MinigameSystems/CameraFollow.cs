using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform;
    public Vector3 offset;
    public float cameraSpeed = 5f;

    void Update()
    {
        Vector3 targetPosition = playerTransform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
}
