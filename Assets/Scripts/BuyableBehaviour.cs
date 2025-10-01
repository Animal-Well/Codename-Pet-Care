using System.Collections;
using UnityEngine;

public class BuyableBehaviour : MonoBehaviour
{
    public BuyableData data;

    private static GameManager gameManager = GameManager.Instance;
    private void Start()
    {
        if (gameManager == null)
            gameManager = GameManager.Instance;
    }
    public void BuyItem()
    {
       gameManager._money -= data.cost;
    }
}
