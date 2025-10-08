using System.Collections;
using UnityEngine;

public class HeldObject : MonoBehaviour
{
    public GameObject[] holdables;
    public ObjectiveCheck targetObjective;
    public GameObject CurrentHeldObject { get; private set; }
    private void ResetHeldObject()
    {
        if(CurrentHeldObject != null)
            Destroy(CurrentHeldObject);
    }
    public void SetHeldObject(string objectName)
    {
        for (int i = 0; i < holdables.Length; i++)
        {
            if (holdables[i].name == objectName)
            {
                ResetHeldObject();
                CurrentHeldObject = Instantiate(holdables[i], transform);
                break;
            }
        }
    }
    public void MoveHeldObject(Vector3 toPos)
    {
        toPos.z = StageManager.Instance.currentMinigame == StageManager.MinigameType.Bathing ? 13 : toPos.z;
        if (CurrentHeldObject != null)
        {
            transform.position = Vector3.Lerp(transform.position, toPos, Time.deltaTime);
        }
    }
}
