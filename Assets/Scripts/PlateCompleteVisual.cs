using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSOGameObject
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject GameObject;
    }
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private List<KitchenObjectSOGameObject> _kitchenObjectSoGameObjectList;

    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (var kitchenObjectSoGameObject in _kitchenObjectSoGameObjectList)
        {
            kitchenObjectSoGameObject.GameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (var kitchenObjectSoGameObject in _kitchenObjectSoGameObjectList)
        {
            if (kitchenObjectSoGameObject.KitchenObjectSO == e.KitchenObjectSO)
            {
                kitchenObjectSoGameObject.GameObject.gameObject.SetActive(true);
            }
        }
    }
}
