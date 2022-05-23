using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ShopUI : MonoBehaviour
{
    [Header("Objects")]
    public TextMeshProUGUI Name;

    [Header("Values")]
    public string ShopName;
    public List<Trade> Trades;

    
    void Start()
    {
        Name.text=ShopName;
    }
}

[Serializable]
public class Trade
{
    public Item ToGive;
    public float Cost;
    public int Stock;
}
