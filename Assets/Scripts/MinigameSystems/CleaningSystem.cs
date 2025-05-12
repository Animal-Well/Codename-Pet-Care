using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningSystem : MonoBehaviour
{
    public GameManager manager;
    public UI_Manager managerUI;

    public Transform soapTransform;
    public Rigidbody soapRb;
    void Start()
    {
        manager = FindFirstObjectByType<GameManager>();
        managerUI = FindFirstObjectByType<UI_Manager>();
    }
    public void ClearPawn()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            soapTransform.position = touchPos;
        }
    }
    public void FinishGame()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other != null && other.CompareTag("Sujeira"))
        {

        }
    }
}
