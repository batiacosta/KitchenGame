using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject _stoveOnGameObject = null;
    [SerializeField] private GameObject _particlesGameObject = null;
    [SerializeField] private StoveCounter _stoveCounter = null;

    private void Start()
    {
        _stoveCounter.OnStateChanged += StoveCounterOnStateChange;
    }

    private void StoveCounterOnStateChange(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.State == StoveCounter.State.Frying || e.State == StoveCounter.State.Fried;
        _stoveOnGameObject.gameObject.SetActive(showVisual);
        _particlesGameObject.gameObject.SetActive(showVisual);
    }
}
