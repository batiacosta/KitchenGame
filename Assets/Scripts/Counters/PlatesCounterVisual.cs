using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform _counterTopPoint;
    [SerializeField] private Transform _plateVisualPrefab;
    [SerializeField] private PlatesCounter _platesCounter;

    private List<GameObject> _plateVisualPlateGameObjectList;

    private void Awake()
    {
        _plateVisualPlateGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        _platesCounter.OnPlateSpawned += PlatesCounter_OnPLateSpawned;
        _platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plateGameObject = _plateVisualPlateGameObjectList[_plateVisualPlateGameObjectList.Count - 1];
        _plateVisualPlateGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPLateSpawned(object sender, EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(_plateVisualPrefab, _counterTopPoint);
        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0f, plateOffsetY * _plateVisualPlateGameObjectList.Count, 0f);
        _plateVisualPlateGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
