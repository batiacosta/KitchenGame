using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO = null;
    [SerializeField] private Transform _counterTopPoint = null;

    private KitchenObject _kitchenObject = null;
    
    // For testing
    [SerializeField] private ClearCounter _secondClearCounter = null;
    [SerializeField] private bool _isTesting = false;

    private void Update()
    {
        if (_isTesting && Input.GetKeyDown(KeyCode.T))
        {
            if (_kitchenObject !=null)
            {
                _kitchenObject.SetClearCounter(_secondClearCounter);
            }
        }
    }

    public void Interact()
    {
        if (_kitchenObject == null) 
        {
            Transform kitchenObjectTransform = Instantiate(_kitchenObjectSO.Prefab, _counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            Debug.Log(_kitchenObject.GetClearCounter());
        }
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return _counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
