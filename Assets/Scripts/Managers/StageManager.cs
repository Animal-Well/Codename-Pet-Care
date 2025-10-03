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
        None = 4,
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
                return bathObjectives;
            case MinigameType.Cleaning:
                return cleanObjectives;
            case MinigameType.Walking:
                return walkObjectives;
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
            NextMinigame();
            yield break;
        }
        else
        {
            StartCoroutine(MinigameCoroutine());
            //yield return new WaitUntil(() => )
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
        int newMinigameIndex = (int)currentMinigame + 1;
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
