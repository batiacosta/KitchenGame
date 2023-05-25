using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "RecipeSO", menuName = "RecipeSO", order = 0)]
    public class RecipeSO : ScriptableObject
    {
        public List<KitchenObjectSO> kitchenObjectSOList;
        public string recipeName;
    }
}