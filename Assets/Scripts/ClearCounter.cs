using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ClearCounter : BaseCounter, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO = null;

    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //  There is no kitchenobject
            if (player.HasKitchenObject())
            {
                //Player has something, so player can put kitchen object on top of this counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //  Player has nothing, so nothing happens
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                //  There is kitchenobject
            }
            else
            {
                //  Player can pickup whatever ther is on top of this counter
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

}
