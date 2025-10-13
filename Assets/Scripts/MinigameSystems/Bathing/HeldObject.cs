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
    public void MoveHeldObject(Vector3 toPos)
    {
        //toPos.z = StageManager.Instance.currentMinigame == StageManager.MinigameType.Bathing ? 13 : toPos.z;
        transform.position = toPos;
    }
    public void UseHeldObject(ObjectiveCheck usedOn)
    {
        if (usedOn.objectiveType == targetObjective)
        {
            usedOn.ClearObjective();
        }
    }
    public void ChangeObjective(string objectiveName)
    {
        targetObjective = (BathMinigame.ObjectiveTypes)Enum.Parse(typeof(BathMinigame.ObjectiveTypes), objectiveName);
    }
}
