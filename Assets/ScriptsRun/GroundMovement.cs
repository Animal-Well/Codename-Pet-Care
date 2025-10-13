using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(0, 0, 8) * Time.deltaTime;
    }
}
