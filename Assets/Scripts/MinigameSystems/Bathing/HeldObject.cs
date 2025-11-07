using System;
using UnityEngine;

public class HeldObject : MonoBehaviour
{
    public GameObject[] holdables;
    public BathMinigame.ObjectiveTypes targetObjective = BathMinigame.ObjectiveTypes.None;
    public GameObject CurrentHeldObject { get; private set; }
    public void ResetHeldObject()
    {
        if(CurrentHeldObject != null)
            Destroy(CurrentHeldObject);
    }
    public void SetHeldObject(GameObject newHeldObject)
    {
        for (int i = 0; i < holdables.Length; i++)
        {
            if (holdables[i] == newHeldObject)
            {
                CurrentHeldObject = Instantiate(holdables[i], transform);
                break;
            }
        }
    }
    public void MoveHeldObject(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            transform.position = hit.point;
            if (hit.collider.TryGetComponent<ObjectiveCheck>(out ObjectiveCheck check))
            {
                UseHeldObject(check);
            }
        }
    }
    public void UseHeldObject(ObjectiveCheck usedOn)
    {
        if (usedOn.objectiveType == targetObjective)
        {
            usedOn.OnDestroyEvent.Invoke();
        }
    }
    public void ChangeObjective(string objectiveName)
    {
        targetObjective = (BathMinigame.ObjectiveTypes)Enum.Parse(typeof(BathMinigame.ObjectiveTypes), objectiveName);
    }
}
