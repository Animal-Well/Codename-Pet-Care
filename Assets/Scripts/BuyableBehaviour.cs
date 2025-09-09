using System.Collections;
using UnityEngine;

public class BuyableBehaviour : MonoBehaviour
{
    public BuyableData data;

    private static GameManager gameManager = GameManager.Instance_GameManager;
    private void Start()
    {
        if (gameManager == null)
            gameManager = GameManager.Instance_GameManager;
    }
    public void BuyItem()
    {
       gameManager.money -= data.cost;
    }
}
