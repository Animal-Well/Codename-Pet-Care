using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static StageManager;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance { get; private set; }

    public static GameManager gameManager = GameManager.Instance;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI playerLevelText;
    public Slider progressBar;

    private void Awake()
    {
        //  N�o tenho certeza se vai ser necessario manter isso como um "Manager"

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
        if (CurrentMinigame == MinigameType.None)
        {
            UpdateText();
            UpdateLevel();
            moneyText = GameObject.FindGameObjectWithTag("Money").GetComponent<TextMeshProUGUI>();
            energyText = GameObject.FindGameObjectWithTag("Energy").GetComponent<TextMeshProUGUI>();
        }
        else if (CurrentMinigame == MinigameType.Bathing)
        {
            progressBar = FindFirstObjectByType<Slider>().GetComponent<Slider>();
        }
    }

    void Update()
    {
        //UseJoystick();
        if (CurrentMinigame == MinigameType.None)
        {
            UpdateText();
            UpdateLevel();
        }
        else if (CurrentMinigame == MinigameType.Bathing)
        {
            progressBar.value = StageManager.Instance.GetProgress();
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
