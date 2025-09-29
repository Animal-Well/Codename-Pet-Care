using System.Collections;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private static GameManager Manager = GameManager.Instance;

    //[Header("Cleaing Minigame")]

    [Header("Bathing Minigame")]
    [SerializeField] private LayerMask bathingLayers;
    [SerializeField] private GameObject[] bathingObjects;
    [SerializeField] private GameObject currentBathObject;
    private bool _canStartCoroutines = true;
    private Ray ray;
    private RaycastHit hit;


    [Header("Walking Minigame")]
    public float speed = 15f;
    private CharacterController _controller;
    void Start()
    {
        if (_controller == null && StageManager.CurrentMinigame == StageManager.MinigameType.Walking)
            _controller = GetComponent<CharacterController>();
        if (Manager == null)
            Manager = GameManager.Instance;
    }
    void Update()
    {
        switch (StageManager.CurrentMinigame)
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
            default:
                Manager.ChangeScene(StageManager.MinigameType.None);
                break;
        }
        if (_canStartCoroutines)
        {
            _canStartCoroutines = false;
            StartCoroutine(ResetBathingObject());
        }
    }
    private void OnBathing()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            switch (StageManager.CurrentStage)
            {
                case StageManager.MinigameStages.Start:
                    UseSoap(hit.point);
                    if (hit.collider.CompareTag("Dirt"))
                    {
                        Destroy(hit.collider.gameObject);
                        StageManager.Instance.GrowMinigameProgress();
                    }
                    break;
                case StageManager.MinigameStages.Middle:
                    UseNailClip(hit.point);
                    if (hit.collider.CompareTag("Nails"))
                    {
                        Destroy(hit.collider.gameObject);
                        StageManager.Instance.GrowMinigameProgress();
                    }
                    break;
                case StageManager.MinigameStages.End:
                    UseShower(hit.point);
                    if (hit.collider.CompareTag("Shower"))
                    {
                        Destroy(hit.collider.gameObject);
                        StageManager.Instance.GrowMinigameProgress();
                    }
                    break;
                default:
                    Manager.ChangeScene(StageManager.MinigameType.None);
                    break;
            }
        }
    }
    private void UseSoap(Vector3 soapPos)
    {
        currentBathObject = currentBathObject == null ? Instantiate(bathingObjects[0]) : currentBathObject;
        TouchToMove(soapPos, currentBathObject.transform);
    }
    private void UseNailClip(Vector3 clipPos)
    {
        currentBathObject = currentBathObject == null ? Instantiate(bathingObjects[1]) : currentBathObject;
        TouchToMove(clipPos, currentBathObject.transform);
    }
    private void UseShower(Vector3 showerPos)
    {
        currentBathObject = currentBathObject == null ? Instantiate(bathingObjects[2]) : currentBathObject;
        TouchToMove(showerPos, currentBathObject.transform);
    }
    private void TouchToMove(Vector3 moveTo, Transform movingObject)
    {
        if (Input.GetButton("Fire1"))
        {
            movingObject.position = moveTo;
        }
    }
    private IEnumerator ResetBathingObject()
    {
        Destroy(currentBathObject);
        StageManager.MinigameStages stageCheck = StageManager.CurrentStage;
        yield return null;
        yield return new WaitUntil(() => stageCheck != StageManager.CurrentStage);
        StartCoroutine(ResetBathingObject());
    }
    private void OnCleaning()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            GameObject target = hit.collider.gameObject;
            
            switch (StageManager.CurrentStage)
            {
                case StageManager.MinigameStages.Start:
                    if (target.CompareTag("ObjectiveCleaning"))
                    {
                        Destroy(hit.collider.gameObject);
                    }
                    break;
                case StageManager.MinigameStages.Middle:
                    if (target.CompareTag("ObjectiveCleaning"))
                    {
                        Destroy(hit.collider.gameObject);
                    }
                    break;
                case StageManager.MinigameStages.End:
                    Manager.ChangeScene(StageManager.MinigameType.None);
                    break;
            }
        }
    }
}
