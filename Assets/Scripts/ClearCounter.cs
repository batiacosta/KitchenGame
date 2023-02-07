using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSO = null;
    [SerializeField] private Transform _counterTopPoint = null;
    public void Interact()
    {
        Transform kitchenObjectSoTransform = Instantiate(_kitchenObjectSO.Prefab, _counterTopPoint);
        kitchenObjectSoTransform.localPosition = Vector3.zero;
        
        Debug.Log((kitchenObjectSoTransform.GetComponent<KitchenObject>().gameObject.name));
    }
}
