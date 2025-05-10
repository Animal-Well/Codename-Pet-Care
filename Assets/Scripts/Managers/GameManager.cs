using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance_GameManager { get; private set; }

    public UI_Manager ui_manager;

    public int energy = 3, money = 0, maxEnergy = 3;
    private void Awake()
    {
        if (Instance_GameManager == null)
        {
            Instance_GameManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        ui_manager = FindFirstObjectByType<UI_Manager>();
    }
    private void Update()
    {
        if (energy > maxEnergy)
        { 
            energy = maxEnergy;
            ui_manager.UpdateText();
        }
    }
    public void BuyInStore(GameObject purchaseID)
    {
        bool sucessPurchase;
        int rewardEnergy = 0;
        int rewardMoney = 0;
        switch (purchaseID.name)
        {
            case "Item":
                sucessPurchase = true;
                rewardEnergy = 1;
                rewardMoney = 10;
                break;
            default:
                sucessPurchase = false;
                break;
        }
        if (sucessPurchase)
        {
            money += rewardMoney;
            energy += rewardEnergy;
            ui_manager.UpdateText();
            purchaseID.SetActive(false);
            rewardEnergy = 0;
            rewardMoney = 0;
        }
        else
        {
            GameObject.Find("Purchase Error").SetActive(true);
        }
    }
    public void ChangeScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
}
