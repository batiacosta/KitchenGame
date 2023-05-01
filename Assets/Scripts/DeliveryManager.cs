using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    
    private List<RecipeSO> _waitingRecipeSOList;
    private float _spawnRecipeTimer;
    private float _spawnRecipeTimerMax = 4f;
    private int _waitingRecipeMax = 4;

    private void Awake()
    {
        Instance = this;
        _waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        _spawnRecipeTimer -= Time.deltaTime;
        if (_spawnRecipeTimer <= 0f)
        {
            _spawnRecipeTimer = _spawnRecipeTimerMax;

            if (_waitingRecipeSOList.Count <= _waitingRecipeMax)
            {
                var range = Random.Range(0, recipeListSO.recipeSOList.Count);
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[range];
                Debug.Log(waitingRecipeSO.recipeName);
                _waitingRecipeSOList.Add(waitingRecipeSO);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < _waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = _waitingRecipeSOList[i];
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {   //  All ingredients on recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {   //  All ingredients on the plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        //  This recipe ingredient was not found in the plate
                        plateContentMatchesRecipe = false;
                    }
                }

                if (plateContentMatchesRecipe)
                {
                    //  Player delivered the correct recipe
                    Debug.Log("Player delivered the correct recipe");
                    _waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
            Debug.Log("The player did not delivered the correct recipe");
        }
    }
}
