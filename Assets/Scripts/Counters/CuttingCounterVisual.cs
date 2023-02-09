using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter _cuttingCounter;
    private Animator _animator;
    private const string CUT = "Cut";
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _cuttingCounter.OnCut += ContainerCounterOnPlayerCut;
    }

    private void ContainerCounterOnPlayerCut(object sender, EventArgs e)
    {
        _animator.SetTrigger(CUT);
    }
}
