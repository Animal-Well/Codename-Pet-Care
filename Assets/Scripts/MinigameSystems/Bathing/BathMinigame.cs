using System.Collections.Generic;
using UnityEngine;

public class BathMinigame : MonoBehaviour
{
    public enum ObjectiveTypes
    {
        None,
        Dirt,
        SoapRemains,
        WaterRemains
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
        _progress.ResetProgress();
    }
}
