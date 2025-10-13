using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public ProgressBehaviour ProgressBarBehaviour { get; private set; }
    public enum MinigameType
    {
        None = 0,
        Bathing = 1,
        Walking = 2
    }
    public MinigameType currentMinigame = MinigameType.None;
    
    [SerializeField] private MinigameType lastMinigameBeforeMenu = MinigameType.Walking;
    
    public ObjectiveCheck[] GetMinigameObjectives()
    {
        return FindObjectsByType<ObjectiveCheck>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }
    [SerializeField] private float delayToChangeMinigame = 1.5f;
    public void ChangeMinigame(MinigameType newMinigame)
    {
        currentMinigame = newMinigame;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        yield return new WaitForSeconds(delayToChangeMinigame);
        if (currentMinigame == lastMinigameBeforeMenu)
        {
            GameManager.Instance.LevelUp(100f);
            ChangeToMenuScene();
            yield return new WaitUntil(() => currentMinigame == MinigameType.None);
            GameManager.Instance.UiEvent.Invoke();
        }
        else
        {
            NextMinigame();
            StartCoroutine(MinigameCoroutine());
        }

        yield break;
    }
    private void NextMinigame()
    {
        int newMinigameIndex = (int)currentMinigame + 1 > 2 ? 0 : (int)currentMinigame + 1;

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
            case MinigameType.Walking:
                sceneChanger.ChangeScene("RunGameplay");
                break;
            default:
                minigame = MinigameType.None;
                ChangeMinigame(minigame);
                break;
        }
    }
    private void ChangeToMenuScene()
    {
        currentMinigame = MinigameType.None;

        ChangeToMinigameScene(currentMinigame);
    }
}
