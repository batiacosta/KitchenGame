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
        Transform kitchenObjectTransform = Instantiate(_kitchenObjectSO.Prefab);
        kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
        OnPlayerGrabObject?.Invoke(this, EventArgs.Empty);
    }

}
