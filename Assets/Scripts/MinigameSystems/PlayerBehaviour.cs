using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Bathing Minigame")]
    [SerializeField] private LayerMask bathingLayers;
    [SerializeField] private GameObject[] bathingObjects;
    [SerializeField] private HeldObject currentHeldObject;
    private Ray ray;
    private RaycastHit hit;


    private StageManager.MinigameType _playingMinigame = StageManager.MinigameType.None;

    private void Start()
    {

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
        var progress = StageManager.Instance.ProgressBarBehaviour;

        var bathObjectives = StageManager.Instance.GetMinigameObjectives();

        var currentObjective = bathObjectives[(int)progress.GetRawProgress()];

        //currentHeldObject = currentHeldObject == null ? Instantiate(HeldObject.GetHeldObject()) : currentHeldObject;

        if (Input.GetButton("Fire1"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, bathingLayers))
            {
                if (hit.collider.gameObject == currentObjective)
                {
                    /*Destroy(currentObjective);
                    StageManager.Instance.GrowMinigameProgress();*/

                    Debug.DrawLine(ray.origin, hit.point);
                    Debug.DebugBreak();
                }
            }
        }
    }
    private void OnCleaning()
    {

    }
}
