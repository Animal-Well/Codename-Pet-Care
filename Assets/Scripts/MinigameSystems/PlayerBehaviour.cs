using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public GameManager manager;

    public float speed = 15f;

    public CharacterController controller;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        //HandleMovement();
    }

    public void MoveLeft()
    {
        Vector3 leftPos = new Vector3(14f, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, leftPos, speed * Time.deltaTime);
    }
    public void MoveRight()
    {
        Vector3 rightPos = new Vector3(22f, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, rightPos, speed * Time.deltaTime);
    }
    /*
    void HandleMovement()
    {
        controller.Move(Vector3.forward * speed * Time.deltaTime);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 move = new Vector2(Mathf.Clamp(touchPos.x, 9f, 22f), 0f);
            controller.Move(move * speed * Time.deltaTime);
            
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            Vector3 move = new Vector3(touchPos.x, 0f, 1f);
            _controller.Move(move * speed * Time.deltaTime);
            Debug.Log(touch.position + "     TouchPos:" + touchPos + "     Move:" + move);
        
        }
        
    }*/
}
