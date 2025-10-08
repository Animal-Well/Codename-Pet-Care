using UnityEngine;

public class BathMinigame : MonoBehaviour
{
    public enum ObjectiveTypes
    {
        Dirt = 1,
        Nail = 2,
        SoapRemains = 3
    }
    private ProgressBehaviour _progress;
    void Start()
    {
        _progress = StageManager.Instance.ProgressBarBehaviour;
        InitiateBathMinigame();
    }
    private void InitiateBathMinigame()
    {
        StageManager.Instance.currentMinigame = StageManager.MinigameType.Bathing;
    }
}
