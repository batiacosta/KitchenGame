using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlashingBarUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;

    private Animator _animator;
    private const string IsFlashing = "IsFlashing";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        _animator.SetBool(IsFlashing, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnProgressAmount = 0.5f;
        bool show = stoveCounter.IsFried() && e.ProgressNormalized >= burnProgressAmount;
        _animator.SetBool(IsFlashing, show);
    }
}
