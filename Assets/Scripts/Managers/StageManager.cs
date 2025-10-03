using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public string[] objectiveTags = new string[3] { "BathObjectives", "CleaningObjectives", "WalkObjectives" };
    private GameObject[] GetObjectsByTag(string tag)
    {
        return GameObject.FindGameObjectsWithTag(tag);
    }

    public ProgressBehaviour ProgressBarBehaviour { get;  private set; }
    public enum MinigameType
    {
        None = 0,
        Bathing = 1,
        Cleaning = 2,
        Walking = 3
    }
    public MinigameType currentMinigame = MinigameType.Bathing;
    public GameObject[] GetMinigameObjectives()
    {
        switch (currentMinigame)
        {
            case MinigameType.Bathing:
                return GetObjectsByTag(objectiveTags[0]);
            case MinigameType.Cleaning:
                return GetObjectsByTag(objectiveTags[1]);
            case MinigameType.Walking:
                return GetObjectsByTag(objectiveTags[2]);
            case MinigameType.None:
                return null;
            default:
                Debug.LogWarning("Need to implement new Minigame");
                return new GameObject[0];
        }
    }
    public bool IsMinigameCompleted()
    {
        if (ProgressBarBehaviour.GetPercentProgress() == 1f)
        {
            return true;
        }
        return false;
    }
    public void ChangeMinigame(MinigameType newMinigame)
    {
        currentMinigame = newMinigame;
    }
    private void Awake()
    {
        CheckInstance();
    }
    private void Start()
    {
        ProgressBarBehaviour = FindFirstObjectByType<ProgressBehaviour>();
        StartCoroutine(MinigameCoroutine());
    }
    private IEnumerator MinigameCoroutine()
    {

        yield return new WaitForEndOfFrame();
        if (IsMinigameCompleted())
        {
            yield return new WaitForSeconds(0.6f);
            NextMinigame();
            yield break;
        }
        else
        {
            yield return new WaitUntil(() => IsMinigameCompleted());
            StartCoroutine(MinigameCoroutine());
        }
    }
    public void GrowMinigameProgress()
    {
        ProgressBarBehaviour.AdvanceProgress(1);
    }
    public void CheckInstance()
    {
        if (currentMinigame != MinigameType.None)
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void NextMinigame()
    {
        int newMinigameIndex = (int)currentMinigame + 1;

        if (newMinigameIndex > 3) newMinigameIndex = 0;

        currentMinigame = (MinigameType)newMinigameIndex;
        ChangeToMinigameScene(currentMinigame);
    }
    private void ChangeToMinigameScene(MinigameType minigame)
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
