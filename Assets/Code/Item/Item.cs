using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Item
{
    public string ItemType="Item";
    public float Size;
    public string Name;
    public float Weight;
}
[Serializable]
public class food:Item
{
    public float FoodValue;
}