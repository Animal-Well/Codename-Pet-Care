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
        heldObject = FindFirstObjectByType<HeldObject>();

        _playingMinigame = StageManager.Instance.currentMinigame;
    }

    void Update()
    {
        switch (_playingMinigame)
        {
            case StageManager.MinigameType.Bathing:
                OnBathing();
                break;
            case StageManager.MinigameType.Walking:
                //Call Walking minigame functions
                break;
        }
    }
    private void OnBathing()
    {
        if(Input.GetButton("Fire1"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && heldObject != null)
            {
                heldObject.MoveHeldObject(hit.point);
                if (hit.collider.TryGetComponent<ObjectiveCheck>(out ObjectiveCheck check))
                {
                    heldObject.UseHeldObject(check);
                }
            }
        }
    }
}
