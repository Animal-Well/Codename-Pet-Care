using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }

    public static GameManager gameManager = GameManager.Instance;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI playerLevelText;

    private void Awake()
    {
                //  Não tenho certeza se vai ser necessario manter isso como um "Manager"
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        gameManager = GameManager.Instance;
        //moneyText = GameObject.FindGameObjectWithTag("Money").GetComponent<TextMeshProUGUI>();
        //energyText = GameObject.FindGameObjectWithTag("Energy").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        //UseJoystick();
        if (GameManager.CurrentMinigame == GameManager.MinigameType.None)
        {
            //UpdateText();
            //UpdateLevel();
        }
        
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
