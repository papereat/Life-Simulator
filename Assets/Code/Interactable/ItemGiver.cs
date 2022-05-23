using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGiver : Interactable
{
    public food toAdd;

    public override void OnHit(PlayerControler PC)
    {
        PC.AddItemTo(true,toAdd);
    }
}
