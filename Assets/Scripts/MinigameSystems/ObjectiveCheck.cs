using UnityEngine;

public class ObjectiveCheck : MonoBehaviour
{
    public BathMinigame.ObjectiveTypes objectiveType = BathMinigame.ObjectiveTypes.Dirt;

    [SerializeField] private bool unlocksNewObjective;
    [SerializeField] private GameObject objectiveToUnlock;

    public void ClearObjective()
    {
        if (objectiveToUnlock != null && unlocksNewObjective)
        {
            objectiveToUnlock.SetActive(unlocksNewObjective);
        }
        StageManager.Instance.ProgressBarBehaviour.AdvanceProgress();
        gameObject.SetActive(false);
    }
}
