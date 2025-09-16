using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public GameObject[] objectives;
    private static GameManager gameManager = GameManager.Instance;

    public enum MinigameStages
    {
        Start,
        Middle,
        End
    }
    public static MinigameStages CurrentStage { get; private set; }
    public enum MinigameType
    {
        None,
        Cleaning,
        Bathing,
        Walking
    }
    public static MinigameType CurrentMinigame = MinigameType.Bathing;

    public static void ChangeMinigame(string minigameName)
    {
        MinigameType newMinigame = MinigameType.None;
        if (Enum.TryParse(minigameName, out newMinigame))
        {
            CurrentMinigame = newMinigame;
        }
    }
    public static void ChangeState(MinigameStages changeTo)
    {
        CurrentStage = changeTo;
    }
    public static void ChangeState(string changeTo)
    {
        MinigameStages newState = CurrentStage;

        if(Enum.TryParse(changeTo, out newState))
            CurrentStage = newState;
    }
    private void Awake()
    {
        if(Instance != null)
        {
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        gameManager = gameManager == null ? GameManager.Instance : gameManager;
    }
    private void Update()
    {
        if (CurrentMinigame == MinigameType.Bathing)
        {
            objectives[0] = GameObject.FindGameObjectWithTag("Dirt");
            objectives[1] = GameObject.FindGameObjectWithTag("Nails");
            objectives[2] = gameObject;
            switch (CurrentStage)
            {
                case MinigameStages.Start:
                    CurrentStage = objectives[0] == null ? MinigameStages.Middle : MinigameStages.Start;
                    break;
                case MinigameStages.Middle:
                    CurrentStage = objectives[1] == null ? MinigameStages.End : MinigameStages.Middle;
                    break;
                case MinigameStages.End:
                    if (objectives[2] == null)
                        gameManager.ChangeScene(MinigameType.None);
                    break;
            }
        }
    }
}
