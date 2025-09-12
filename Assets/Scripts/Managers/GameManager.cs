using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public enum MinigameType
    {
        None,
        Cleaning,
        Bathing,
        Walking
    }
    public static MinigameType CurrentMinigame = MinigameType.Cleaning;

    public static void ChangeMinigame(string minigameName)
    {
        MinigameType newMinigame = MinigameType.None;
        if (Enum.TryParse(minigameName, out newMinigame))
        {
            CurrentMinigame = newMinigame;
        }
    }

    public UI_Manager ui_manager;
    public PlayerBehaviour player;

    public int energy = 3, money = 0, maxEnergy = 3;
    public int level = 0;
    public float xpPoints = 0f, xpToLvlUp = 100f;
    public float energyReloadCooldown = 2f;
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
    private void Start()
    {
        ui_manager = UI_Manager.Instance;
        player = FindFirstObjectByType<PlayerBehaviour>();
        StartCoroutine(RechargeEnergy());
    }
    private void Update()
    {
        if (energy > maxEnergy)
        { 
            energy = maxEnergy;
            ui_manager.UpdateText();
        }
    }

    public void LevelUp(int xpGained)
    {
        if (xpPoints == xpToLvlUp)
        {
            level++;
            xpPoints = 0;
            ui_manager.UpdateLevel();
            MaxXpUpdate();
        }
        else if (xpPoints > xpToLvlUp)
        {
            level++;
            xpPoints = xpPoints - xpToLvlUp;
            ui_manager.UpdateLevel();
            LevelUp(0);
            MaxXpUpdate();
        }
        else if (xpPoints < xpToLvlUp)
        {
            xpPoints += xpGained;
            LevelUp(0);
        }
    }
    public void MaxXpUpdate()
    {
        if (level == 0)
            xpToLvlUp = 100f;
        else
        {
            xpToLvlUp = xpToLvlUp + level;
        }
    }

    public void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
    public void ChangeScene(MinigameType minigame)
    {
        switch(minigame)
        {
            case MinigameType.None:
                ChangeScene("Menu");
                break;
            case MinigameType.Bathing:
                ChangeScene("Minigame Banho");
                break;
            case MinigameType.Cleaning:
                ChangeScene("Minigame Arrumar");
                break;
            case MinigameType.Walking:
                ChangeScene("Minigame Caminhar");
                break;
        }
    }
    public void SpendEnergy()
    {
        if(energy < 1)
        {
            Debug.Log("Sem Energia");
        }
    }
    private IEnumerator RechargeEnergy()
    {
        if(energy < maxEnergy)
            energy++;
        yield return new WaitForSecondsRealtime(energyReloadCooldown);
        StartCoroutine(RechargeEnergy());
        yield break;
    }
}
