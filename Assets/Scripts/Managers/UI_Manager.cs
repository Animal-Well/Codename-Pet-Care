using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance_UI_Manager { get; private set; }

    public GameManager gameManager;

    public PlayerBehaviour player;
    public Transform joystick;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI playerLevelText;

    public Transform selectedScreenIndicator;

    private void Awake()
    {
                //  Não tenho certeza se vai ser necessario manter isso como um "Manager"
        
        if (Instance_UI_Manager == null)
        {
            Instance_UI_Manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        moneyText = GameObject.FindGameObjectWithTag("Money").GetComponent<TextMeshProUGUI>();
        energyText = GameObject.FindGameObjectWithTag("Energy").GetComponent<TextMeshProUGUI>();
        selectedScreenIndicator = GameObject.FindGameObjectWithTag("SelectIndicator").GetComponent<Transform>();
        UpdateText();
    }

    void Update()
    {
        UseJoystick();
    }

    public void UpdateText()
    {
        moneyText.text = gameManager.money.ToString();
        energyText.text = gameManager.energy.ToString() + "/3";
    }
    public void UpdateLevel()
    {
        if(gameManager.level > 9)
            playerLevelText.text = gameManager.level.ToString();
        else if(gameManager.level < 10)
            playerLevelText.text = "0" + gameManager.level.ToString();
    }
    public void ChangeSelected(Transform referenceTransform)
    {
        selectedScreenIndicator.position = referenceTransform.position;
    }

    void UseJoystick()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            /*Vector2 move = new Vector2(touchPos.x, touchPos.y);
            joystick.position = new Vector2(Mathf.Clamp(move.x, -50f,50f), 0f);
            Debug.Log(touch.position + "     TouchPos:" + touchPos + "     Move:" + move);*/
        }
            /*
            _controller.Move(Vector3.forward * speed * Time.deltaTime);
                
                
                _controller.Move(move * speed * Time.deltaTime);

                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                Vector3 move = new Vector3(touchPos.x, 0f, 1f);
                _controller.Move(move * speed * Time.deltaTime);
                Debug.Log(touch.position + "     TouchPos:" + touchPos + "     Move:" + move);
            */
        }

}
