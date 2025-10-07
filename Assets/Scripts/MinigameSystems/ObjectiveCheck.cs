using UnityEngine;

public class ObjectiveCheck : MonoBehaviour
{
    public int objectiveIndex;

    private void OnDisable()
    {
        Debug.Log("Disabled");
        StageManager.Instance.GrowMinigameProgress();
    }
}
