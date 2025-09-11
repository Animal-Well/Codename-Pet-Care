using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public GameObject[] objectives;
    private GameManager gameManager;

    public enum MinigameStages
    {
        Start,
        Middle,
        End
    }
    public static MinigameStages CurrentStage;
    private void Awake()
    {
        if(Instance != null)
        {
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
    }
    private void Update()
    {
        if (GameManager.CurrentMinigame == GameManager.MinigameType.Bathing)
        {
            objectives[0] = GameObject.FindGameObjectWithTag("Dirt");
            objectives[1] = GameObject.FindGameObjectWithTag("Nails");
            switch (CurrentStage)
            {
                case MinigameStages.Start:
                    CurrentStage = objectives[0] == null ? MinigameStages.Middle : MinigameStages.Start;
                    break;
                case MinigameStages.Middle:
                    CurrentStage = objectives[1] == null ? MinigameStages.End : MinigameStages.Middle;
                    break;
                case MinigameStages.End:
                    if (objectives[2] == null)
                        gameManager.ChangeScene(GameManager.MinigameType.None);
                    break;
            }
        }
    }
}
