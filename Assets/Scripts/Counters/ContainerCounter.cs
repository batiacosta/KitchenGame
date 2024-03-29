using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent
{

    [SerializeField] private KitchenObjectSO _kitchenObjectSO = null;

    public event EventHandler OnPlayerGrabObject;
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(_kitchenObjectSO, player);
            OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
        }
        
    }

}
