using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private static GameManager manager = GameManager.Instance;

    //[Header("Cleaing Minigame")]

    [Header("Bathing Minigame")]
    [SerializeField] private LayerMask bathingLayers;
    [SerializeField] private GameObject[] bathingObjects;
    [SerializeField] private GameObject currentBathObject;
    private Ray ray;
    private RaycastHit hit;


    [Header("Walking Minigame")]
    public float speed = 15f;
    private CharacterController _controller;
    void Start()
    {
        if (_controller == null && GameManager.CurrentMinigame == GameManager.MinigameType.Walking)
        _controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        switch(GameManager.CurrentMinigame)
        {
            case GameManager.MinigameType.Bathing:
                OnBathing();   //Call Bathing minigame functions
                break;
            case GameManager.MinigameType.Cleaning:
                //Call Cleaning minigame functions
                break;
            case GameManager.MinigameType.Walking:
                //Call Walking minigame functions
                break;
            default:
                //Do nothing
                break;
        }
    }
    private void OnBathing()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, 1000f, bathingLayers))
        {
            switch(StageManager.CurrentStage)
            {
                case StageManager.MinigameStages.Start:
                    if(Input.GetButton("Fire1"))
                    {
                        UseSoap(hit.point);
                        if(hit.collider.CompareTag("Dirt"))
                        {
                            Destroy(hit.collider.gameObject);
                        }
                    }
                    break;
                case StageManager.MinigameStages.Middle:
                    if(Input.GetButtonDown("Fire1"))
                    {
                        if(hit.collider.CompareTag("Nails"))
                            UseNailClip(hit.collider.gameObject);
                    }
                    break;
                default:
                    break;
            }
        }
    }
    private void UseSoap(Vector3 soapPos)
    {
        currentBathObject = currentBathObject == null ? Instantiate(bathingObjects[0]) : currentBathObject;
        currentBathObject.transform.position = soapPos;
    }
    private void UseNailClip(GameObject target)
    {
        Destroy(target);
    }

}
