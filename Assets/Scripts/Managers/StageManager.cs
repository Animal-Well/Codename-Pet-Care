using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public string[] objectiveTags;
    private GameObject[] GetObjectivesByTag(string tag)
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
                return GetObjectivesByTag(objectiveTags[0]);
            case MinigameType.Cleaning:
                return GetObjectivesByTag(objectiveTags[1]);
            case MinigameType.Walking:
                return GetObjectivesByTag(objectiveTags[2]);
            case MinigameType.None:
                return null;
            default:
                Debug.LogWarning("Need to implement new Minigame");
                return new GameObject[0];
        }
    }
    public bool IsMinigameCompleted()
    {
        return ProgressBarBehaviour.GetPercentProgress() == 1f;
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
        yield return new WaitUntil(() => IsMinigameCompleted());
        NextMinigame();
        yield break;
    }
    public void GrowMinigameProgress()
    {
        ProgressBarBehaviour.AdvanceProgress(1);
        GetMinigameObjectives()[3].SetActive(!GetMinigameObjectives()[0].activeInHierarchy);
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
                //DontDestroyOnLoad(Instance);
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void NextMinigame()
    {
        int newMinigameIndex = (int)currentMinigame + 1 > 3 ? 0 : (int)currentMinigame + 1;

        currentMinigame = (MinigameType)newMinigameIndex;

        GameManager.Instance.LevelUp(100f);

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
