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
    public static MinigameType CurrentMinigame = MinigameType.Cleaning;
    [SerializeField] private float _maxProgress = 3f;
    [SerializeField] private float _progress = 0f;

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
        _progress = 0;
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
    public void NextStage()
    {
        if (CurrentStage == MinigameStages.Start)
        {
            CurrentStage = MinigameStages.Middle;
            _progress++;
        }
        else
        {
            CurrentStage = MinigameStages.End;
            _progress++;
        }
    }
    public float GetProgress()
    {
        return _progress / _maxProgress;
    }
    private void Start()
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
        gameManager = GameManager.Instance;
        StartCoroutine(MinigameCoroutine());
    }
    private IEnumerator MinigameCoroutine()
    {
        switch (CurrentMinigame)
        {
            case MinigameType.Bathing:
                CurrentObjectives = bathObjectives;
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
        
        switch (CurrentStage)
        {
            case MinigameStages.Start:
                if (CurrentObjectives[0] == null)
                {
                    NextStage();
                }
                break;
            case MinigameStages.Middle:
                if (CurrentObjectives[1] == null)
                {
                    NextStage();
                }
                break;
            case MinigameStages.End:
                if (CurrentMinigame == MinigameType.Bathing)
                {
                    if (CurrentObjectives[2] != null && !CurrentObjectives[2].activeSelf)
                        CurrentObjectives[2].SetActive(true);
                    else if (CurrentObjectives[2] == null)
                    {
                        _progress++;
                    }
                }
            break;
        }

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(MinigameCoroutine());
    }
}
