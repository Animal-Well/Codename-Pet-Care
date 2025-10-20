using System.Collections;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public ProgressBehaviour ProgressBarBehaviour { get; private set; }
    
    public ObjectiveCheck[] GetMinigameObjectives()
    {
        return FindObjectsByType<ObjectiveCheck>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }
    [SerializeField] private float delayToChangeMinigame = 1.5f;
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
        ChangeToMenuScene();
        yield break;
    }
    private void ChangeToMenuScene()
    {
        GameManager.Instance.ChangeScene("Menu");
    }
}
