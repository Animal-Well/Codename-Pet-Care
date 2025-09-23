using System;
using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public GameObject[] CurrentObjectives { get; private set; }
    [SerializeField] private GameObject[] bathObjectives = new GameObject[3];
    [SerializeField] private GameObject[] cleanObjectives = new GameObject[3];
    [SerializeField] private GameObject[] walkObjectives = new GameObject[3];
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
    public float _progress;

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
    public float GetProgress()
    {
        return _progress /= CurrentObjectives.Length;
    }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }
    private void Start()
    {
        gameManager = gameManager == null ? GameManager.Instance : gameManager;
        StartCoroutine(MinigameCoroutine());
    }
    private IEnumerator MinigameCoroutine()
    {
        if (CurrentMinigame == MinigameType.Bathing)
        {
            CurrentObjectives = bathObjectives;
            switch (CurrentStage)
            {
                case MinigameStages.Start:
                    if (CurrentObjectives[0] == null)
                    {
                        CurrentStage = MinigameStages.Middle;
                    }
                    break;
                case MinigameStages.Middle:
                    if (CurrentObjectives[1] == null)
                    {
                        CurrentStage = MinigameStages.End;
                    }
                    break;
                case MinigameStages.End:
                    CurrentObjectives[2].SetActive(true);
                    break;
            }
        }
        else if (CurrentMinigame == MinigameType.Cleaning)
        {
            CurrentObjectives = cleanObjectives;
            switch (CurrentStage)
            {
                case MinigameStages.Start:
                    if (CurrentObjectives[0] == null)
                    {
                        CurrentStage = MinigameStages.Middle;
                        _progress++;
                    }
                    break;
                case MinigameStages.Middle:
                    if (CurrentObjectives[1] == null)
                    {
                        CurrentStage = MinigameStages.End;
                        _progress++;
                    }
                    break;
                case MinigameStages.End:
                    if (CurrentObjectives[2] == null)
                    {
                        _progress++;
                        gameManager.ChangeScene(MinigameType.None);
                    }
                    break;
            }
        }
        else if (CurrentMinigame == MinigameType.Walking)
        {
            CurrentObjectives = walkObjectives;
            switch (CurrentStage)
            {
                case MinigameStages.Start:
                    CurrentStage = CurrentObjectives[0] == null ? MinigameStages.Middle : MinigameStages.Start;
                    break;
                case MinigameStages.Middle:
                    CurrentStage = CurrentObjectives[1] == null ? MinigameStages.End : MinigameStages.Middle;
                    break;
                case MinigameStages.End:
                    CurrentObjectives[2].SetActive(true);
                    if (CurrentObjectives[2] == null)
                        gameManager.ChangeScene(MinigameType.None);
                    break;
            }
        }
        else
        {
            CurrentObjectives = new GameObject[3];
            CurrentStage = MinigameStages.Start;
            CurrentMinigame = MinigameType.None;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(MinigameCoroutine());
    }
}
