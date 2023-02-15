using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs
    {
        public KitchenObjectSO KitchenObjectSO;
    }
    [SerializeField] private List<KitchenObjectSO> _validKitchenObjectSOList;
    private List<KitchenObjectSO> _kitchenObjectList;

    private void Awake()
    {
        _kitchenObjectList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (!_validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;   //  Not a valid ingredient
        }
        if (_kitchenObjectList.Contains(kitchenObjectSO))
        {
            //  Already has this type
            return false;
        }
        else
        {
            _kitchenObjectList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs{KitchenObjectSO = kitchenObjectSO});
            return true;
        }
        
    }
}
