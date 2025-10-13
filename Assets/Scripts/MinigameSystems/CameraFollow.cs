using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 offset;
    public float cameraSpeed = 5f;

    void LateUpdate()
    {
        Vector3 targetPosition = targetTransform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
}
