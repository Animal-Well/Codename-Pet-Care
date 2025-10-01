using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    
    [SerializeField] private GameObject[] bathObjectives = new GameObject[3];
    [SerializeField] private GameObject[] cleanObjectives = new GameObject[3];
    [SerializeField] private GameObject[] walkObjectives = new GameObject[3];

    public ProgressBehaviour ProgressBarBehaviour { get;  private set; }
    public enum MinigameType
    {
        None = 0,
        Bathing = 1,
        Cleaning = 2,
        Walking = 3
    }
    public MinigameType currentMinigame = MinigameType.None;
    public GameObject[] GetMinigameObjectives()
    {
        if (currentMinigame != MinigameType.None)
        {
            if (currentMinigame == MinigameType.Bathing)
                return bathObjectives;
            else if (currentMinigame == MinigameType.Cleaning)
                return cleanObjectives;
            else if (currentMinigame == MinigameType.Walking)
                return walkObjectives;
            else
                return new GameObject[3];
        }
        else
            return new GameObject[3];
    }
    private bool IsMinigameCompleted()
    {
        if (ProgressBarBehaviour.GetPercentProgress() == 1f)
        {
            return true;
        }
        return false;
    }
    public void ChangeMinigame(MinigameType newMinigame)
    {
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
        ProgressBarBehaviour = FindFirstObjectByType<ProgressBehaviour>();
        StartCoroutine(MinigameCoroutine());
    }
    private IEnumerator MinigameCoroutine()
    {

        /*
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
                    _progressBarBehaviour.AdvanceProgress(1f);
                    CurrentStage = MinigameStages.End;
                }
                break;
            case MinigameStages.End:
                if (currentMinigame == MinigameType.Bathing)
                {
                    if (CurrentObjectives[2] == null)
                    {
                        _progress++;
                        gameManager.ChangeScene(MinigameType.None);
                    }

                    if (CurrentObjectives[2] != null && !CurrentObjectives[2].activeSelf)
                        CurrentObjectives[2].SetActive(true);
                }
                break;
        }
        */

        yield return new WaitForEndOfFrame();
        if (IsMinigameCompleted())
        {
            StartCoroutine(MinigameCoroutine());
        }
        else
        {
            //yield return new WaitUntil(() => )
        }

    }
    public void GrowMinigameProgress()
    {
        ProgressBarBehaviour.AdvanceProgress(1);
    }
    private void NextMinigame()
    {
        if (currentMinigame != MinigameType.None)
        {
            int newMinigame = (int)currentMinigame + 1;
            ChangeToMinigameScene((MinigameType)newMinigame);
        }
        else
        {
            ChangeToMinigameScene(MinigameType.None);
        }
    }
    public void ChangeToMinigameScene(MinigameType minigame)
    {
        var sceneChanger = GameManager.Instance;
        if (minigame != currentMinigame)
        {
            currentMinigame = minigame;
            ProgressBarBehaviour.ResetProgress();
        }

        switch (currentMinigame)
        {
            case MinigameType.None:
                Debug.Log("No minigame was set. Returning to Menu.");
                sceneChanger.ChangeScene("Menu");
                break;
            case MinigameType.Bathing:
                sceneChanger.ChangeScene("Minigame Banho");
                break;
            case MinigameType.Cleaning:
                sceneChanger.ChangeScene("Minigame Arrumar");
                break;
            case MinigameType.Walking:
                sceneChanger.ChangeScene("Minigame Caminhar");
                break;
            default:
                minigame = MinigameType.None;
                ChangeMinigame(minigame);
                break;
        }
    }
}
