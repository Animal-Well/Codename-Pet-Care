using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 offset;
    public float cameraSpeed = 5f;

    void Update()
    {
        Vector3 targetPosition = targetTransform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
    public void NewRoom(Vector3 roomPos)
    {
        Vector3 targetPosition = roomPos + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
}
