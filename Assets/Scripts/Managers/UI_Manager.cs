using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }

    private GameManager gameManager;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI playerLevelText;

    private void Awake()
    {
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
        if (StageManager.Instance.currentMinigame == StageManager.MinigameType.None)
        {
            UpdateText();
            UpdateLevel();
            moneyText = GameObject.FindGameObjectWithTag("Money").GetComponent<TextMeshProUGUI>();
            energyText = GameObject.FindGameObjectWithTag("Energy").GetComponent<TextMeshProUGUI>();
        }
    }

    void LateUpdate()
    {
        if (StageManager.Instance.currentMinigame == StageManager.MinigameType.None)
        {
            UpdateText();
            UpdateLevel();
        }
    }

    public void UpdateText()
    {
        moneyText.text = gameManager.GetMoney().ToString();
        energyText.text = $"{gameManager.GetRawEnergy()}/{gameManager.GetMaxEnergy()}";
    }
    public void UpdateLevel()
    {
        playerLevelText.text = gameManager.GetCurrentLevel().ToString();
    }
}
