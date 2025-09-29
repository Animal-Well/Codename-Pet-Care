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
    public bool isCompleted = false;

    private ProgressBehaviour _progressBarBehaviour;

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
        if (Enum.TryParse(minigameName, out MinigameType newMinigame))
        {
            CurrentMinigame = newMinigame;
        }
    }
    public void ChangeMinigame(MinigameType newMinigame)
    {
        CurrentMinigame = newMinigame;
        CurrentStage = MinigameStages.Start;
        _progressBarBehaviour.ResetProgress();
    }
    public static void ChangeState(MinigameStages changeTo)
    {
        CurrentStage = changeTo;
    }
    public static void ChangeState(string changeTo)
    {
        if(Enum.TryParse(changeTo, out MinigameStages newState))
            CurrentStage = newState;
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
        gameManager = GameManager.Instance;
        _progressBarBehaviour = FindFirstObjectByType<ProgressBehaviour>();
        StartCoroutine(MinigameCoroutine());
    }
    private void LateUpdate()
    {
        
    }
    private IEnumerator MinigameCoroutine()
    {
        CheckMinigame();
        MinigameStages currentState = CurrentStage;
        switch (CurrentStage)
        {
            case MinigameStages.Start:
                if (CurrentObjectives[0] == null)
                {
                    _progressBarBehaviour.AdvanceProgress(1f);
                    CurrentStage = MinigameStages.Middle;
                }
                break;
            case MinigameStages.Middle:
                if (CurrentObjectives[1] == null)
                {
                    _progressBarBehaviour.AdvanceProgress(1f);
                    CurrentStage = MinigameStages.End;
                }
                break;
            case MinigameStages.End:
                if (CurrentMinigame == MinigameType.Bathing)
                {
                    if (CurrentObjectives[2] == null)
                    {
                        _progressBarBehaviour.AdvanceProgress(1f);
                    }

                    if (CurrentObjectives[2] != null && !CurrentObjectives[2].activeSelf)
                        CurrentObjectives[2].SetActive(true);
                }
                break;
        }

        yield return new WaitForEndOfFrame();
        if (isCompleted)
        {
            isCompleted = false;
            yield return new WaitForEndOfFrame();
            StartCoroutine(MinigameCoroutine());
        }
        else
        {
            StartCoroutine(MinigameCoroutine());
        }
            
    }
    private void CheckMinigame()
    {
        switch (CurrentMinigame)
        {
            case MinigameType.Bathing:
                CurrentObjectives = bathObjectives;
                _progressBarBehaviour.SetMaxProgress(CurrentObjectives.Length); 

                if (_progressBarBehaviour.GetProgress() == 1)
                {
                    CurrentMinigame = MinigameType.Cleaning;
                    gameManager.ChangeScene(CurrentMinigame);
                }
                break;
            case MinigameType.Cleaning:
                CurrentObjectives = cleanObjectives;
                break;
            case MinigameType.Walking:
                CurrentObjectives = walkObjectives;
                break;
            default:
                CurrentObjectives = new GameObject[3];
                CurrentStage = MinigameStages.Start;
                CurrentMinigame = MinigameType.None;
                break;
        }
    }
    public void GrowMinigameProgress()
    {
        isCompleted = true;
    }
}
