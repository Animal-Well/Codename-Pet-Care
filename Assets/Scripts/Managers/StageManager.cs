using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public ProgressBehaviour ProgressBarBehaviour { get;  private set; }
    public enum MinigameType
    {
        None = 0,
        Bathing = 1,
        Cleaning = 2,
        Walking = 3
    }
    public MinigameType currentMinigame = MinigameType.None;
    public ObjectiveCheck[] GetMinigameObjectives()
    {
        return FindObjectsByType<ObjectiveCheck>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }
    public void ChangeMinigame(MinigameType newMinigame)
    {
        currentMinigame = newMinigame;
    }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
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

        yield return new WaitForEndOfFrame();
        yield return new WaitUntil(() => ProgressBarBehaviour.IsProgressComplete());
        NextMinigame();
        yield break;
    }
    public void CheckInstance()
    {
        if (currentMinigame != MinigameType.None)
        {
            
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
