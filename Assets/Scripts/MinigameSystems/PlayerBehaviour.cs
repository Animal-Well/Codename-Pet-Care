using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private static GameManager manager = GameManager.Instance_GameManager;

    //[Header("Cleaing Minigame")]

    [Header("Bathing Minigame")]
    [SerializeField] private LayerMask bathingLayers;
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
            switch(GameManager.CurrentStage)
            {
                default:
                    break;
            }
        }
    }
    private void ClipingNails(Animator animator)
    {
        animator.SetTrigger("ClipNail");
    }

}
