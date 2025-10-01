using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //[Header("Cleaing Minigame")]

    [Header("Bathing Minigame")]
    [SerializeField] private LayerMask bathingLayers;
    [SerializeField] private GameObject[] bathingObjects;
    [SerializeField] private GameObject currentBathObject;
    //private bool _canStartCoroutines = true;
    private Ray ray;
    private RaycastHit hit;
    private StageManager.MinigameType _playingMinigame = StageManager.MinigameType.None;

    [Header("Walking Minigame")]
    public float speed = 15f;
    [SerializeField] private CharacterController _controller;
    void Start()
    {
        if (StageManager.Instance.currentMinigame == StageManager.MinigameType.Walking)
            _controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (Application.isFocused)
        {
            if (Application.isEditor)
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            else if (Input.touchCount > 0)
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            else
                return;

            _playingMinigame = StageManager.Instance.currentMinigame;

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
    }
    private void OnBathing()
    {
        var progress = StageManager.Instance.ProgressBarBehaviour;

        var bathObjectives = StageManager.Instance.GetMinigameObjectives();

        if (Physics.Raycast(ray, out hit, 100f, bathingLayers))
        {
            if (hit.collider.gameObject == bathObjectives[(int)progress.GetRawProgress()])
            {
                Destroy(hit.collider.gameObject);
            }
        }

        switch (progress.GetRawProgress())
        {
            case 0:
                if (currentBathObject != bathingObjects[0])
                {
                    ResetHeldObject();
                    currentBathObject = Instantiate(bathingObjects[0], transform);
                }
                break;
            case 1:
                if (currentBathObject != bathingObjects[1])
                {
                    ResetHeldObject();
                    currentBathObject = Instantiate(bathingObjects[1], transform);
                }
                break;
            case 2:
                if (currentBathObject != bathingObjects[2])
                {
                    ResetHeldObject();
                    currentBathObject = Instantiate(bathingObjects[2], transform);
                }
                break;
                /*
            default:
                if (_canStartCoroutines)
                {
                    _canStartCoroutines = false;
                    StartCoroutine(EndMinigameCoroutine());
                }
                break;*/
        }
        MoveHeldObject();
    }
    private void ResetHeldObject()
    {
        Destroy(currentBathObject);
    }
    private void MoveHeldObject()
    {
        if (currentBathObject != null)
        {
            currentBathObject.transform.position = Vector3.Lerp(currentBathObject.transform.position, hit.point, 0.2f);
        }
    }
    private void OnCleaning()
    {

    }
}
