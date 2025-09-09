using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance_UI_Manager { get; private set; }

    public static GameManager gameManager = GameManager.Instance_GameManager;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI playerLevelText;

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
        gameManager = GameManager.Instance_GameManager;
    }
    void Start()
    {
        moneyText = GameObject.FindGameObjectWithTag("Money").GetComponent<TextMeshProUGUI>();
        energyText = GameObject.FindGameObjectWithTag("Energy").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //UseJoystick();
        UpdateText();
        UpdateLevel();
    }

    public void UpdateText()
    {
        moneyText.text = gameManager.money.ToString();
        energyText.text = $"{gameManager.energy}/{gameManager.maxEnergy}";
    }
    public void UpdateLevel()
    {
        playerLevelText.text = gameManager.level.ToString();
    }
}
