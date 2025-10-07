using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Bathing Minigame")]
    [SerializeField] private HeldObject heldObject;
    private GameObject[] _allObjectives;
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
        _allObjectives = StageManager.Instance.GetMinigameObjectives();
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

        Debug.Log(_progress.GetRawProgress());

        var currentObjective = _allObjectives[(int)_progress.GetRawProgress()];

        Debug.Log(currentObjective);

        if (Input.GetButton("Fire1"))
        {
            if (!heldObject.enabled)
                heldObject.enabled = true;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                heldObject.MoveHeldObject(hit.point);
                if (hit.collider.gameObject == currentObjective)
                {
                    hit.collider.gameObject.SetActive(false);
                }
            }
        }
        else if (heldObject.enabled)
        {
            heldObject.enabled = false;
        }
    }
    private void OnCleaning()
    {

    }
}
