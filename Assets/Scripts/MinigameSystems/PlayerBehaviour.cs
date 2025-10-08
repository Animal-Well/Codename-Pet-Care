using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Bathing Minigame")]
    [SerializeField] private HeldObject heldObject;
    private ProgressBehaviour _progress;
    private Ray ray;
    private RaycastHit hit;


    private StageManager.MinigameType _playingMinigame = StageManager.MinigameType.None;
    private void Start()
    {
        heldObject = GetComponent<HeldObject>();

        _playingMinigame = StageManager.Instance.currentMinigame;
    }

    void Update()
    {
        switch (_playingMinigame)
        {
            case StageManager.MinigameType.Bathing:
                OnBathing();
                break;
            case StageManager.MinigameType.Cleaning:
                OnCleaning();
                break;
            case StageManager.MinigameType.Walking:
                //Call Walking minigame functions
                break;
        }
    }
    private void OnBathing()
    {
        _progress = StageManager.Instance.ProgressBarBehaviour;
        Debug.Log($"Progress: {_progress.GetRawProgress()}");

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            heldObject.MoveHeldObject(hit.point);
            if (hit.collider.TryGetComponent<ObjectiveCheck>(out ObjectiveCheck check))
            {
                //check.gameObject.SetActive(!(check.objectiveType == heldObject.targetObjective));
            }
        }
    }
    private void OnCleaning()
    {

    }
}
