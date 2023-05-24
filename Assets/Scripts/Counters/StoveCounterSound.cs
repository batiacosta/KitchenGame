using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    
    private AudioSource _audioSource;
    private float _warningSoundTimer;
    private bool _playingWarningSound;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void Update()
    {
        if (_playingWarningSound)
        {
            _warningSoundTimer -= Time.deltaTime;
            if (_warningSoundTimer <= 0f)
            {
                float warningSoundTimerMax = 0.2f;
                _warningSoundTimer = warningSoundTimerMax;
                
            }
        }
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnProgressAmount = 0.5f;
        bool _playingWarningSound = stoveCounter.IsFried() && e.ProgressNormalized >= burnProgressAmount;
        SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.State == StoveCounter.State.Frying || e.State == StoveCounter.State.Fried;
        if (playSound)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Pause();
        }
    }
}
