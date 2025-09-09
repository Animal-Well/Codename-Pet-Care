using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewBuyable", menuName = "Game/Buyable")]
public class BuyableData : ScriptableObject
{
    public enum BuyableTypes
    {
        Money,
        Energy
    }
    public int cost;
    public int reward;
    public BuyableTypes type;
}
