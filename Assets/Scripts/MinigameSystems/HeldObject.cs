using System.Collections;
using UnityEngine;

public class HeldObject : MonoBehaviour
{
    public GameObject[] holdables;
    public GameObject CurrentHeldObject { get; private set; }
    public static HeldObject GetHeldObject()
    {
        return null;
    }

    public void ResetHeldObject()
    {
        Destroy(CurrentHeldObject);

    }
    public void MoveHeldObject(Vector3 toPos)
    {
        if (CurrentHeldObject != null)
        {
            CurrentHeldObject.transform.position = Vector3.Lerp(CurrentHeldObject.transform.position, toPos, Time.deltaTime);
        }
    }
    public IEnumerator CheckHeldObject()
    {
        yield break;
    }
}
