using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private CuttingRecipeSO[] _cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
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
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObject)
    {
        foreach (var cuttingRecipeSO in _cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.Input == inputKitchenObject)
            {
                return cuttingRecipeSO.Output;
            }
        }
        return null;
    }
}
