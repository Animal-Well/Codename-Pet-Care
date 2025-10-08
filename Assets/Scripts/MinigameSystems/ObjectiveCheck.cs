using UnityEngine;

public class ObjectiveCheck : MonoBehaviour
{
    public BathMinigame.ObjectiveTypes objectiveType = BathMinigame.ObjectiveTypes.Dirt;

    private void OnDisable()
    {
        Debug.Log("Disabled");
        StageManager.Instance.ProgressBarBehaviour.AdvanceProgress();
    }
}
