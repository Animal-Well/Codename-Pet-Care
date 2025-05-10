using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance_UI_Manager { get; private set; }

    public GameManager gameManager;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI energyText;

    public Transform selectedScreenIndicator;

    private void Awake()
    {
                //  Não tenho certeza se vai ser necessario manter isso como um "Manager"
        /*
        if (Instance_UI_Manager == null)
        {
            Instance_UI_Manager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        */
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
        
    }

    public void UpdateText()
    {
        moneyText.text = gameManager.money.ToString();
        energyText.text = gameManager.energy.ToString() + "/3";
    }
    public void ChangeSelected(Transform referenceTransform)
    {
        selectedScreenIndicator.position = referenceTransform.position;
    }
}
