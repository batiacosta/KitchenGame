using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler OnStateChanged; 
    private enum State
    {
        WaitingToStart, //  Waiting until all players are connected
        CountdownToStart,
        GamePlaying,
        GameOver,
    }

    private State _state;
    private float _waitingToStartTimer = 1f;
    private float _countdownToStartTimer = 3f;
    private float _gamePlayingTimer;
    private float _gamePlayingTimerMax = 10f;
    
    private void Awake()
    {
        Instance = this;
        _state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (_state)
        {
         case State.WaitingToStart:
             _waitingToStartTimer -= Time.deltaTime;
             if (_waitingToStartTimer < 0f)
             {
                 _state = State.CountdownToStart;
                 OnStateChanged?.Invoke(this, EventArgs.Empty);
             }
             break;
         case State.CountdownToStart:
             _countdownToStartTimer -= Time.deltaTime;
             if (_countdownToStartTimer < 0f)
             {
                 _state = State.GamePlaying;
                 _gamePlayingTimer = _gamePlayingTimerMax;
                 OnStateChanged?.Invoke(this, EventArgs.Empty);
             }
             break;
         case State.GamePlaying:
             _gamePlayingTimer -= Time.deltaTime;
             if (_gamePlayingTimer < 0f)
             {
                 _state = State.GameOver;
                 OnStateChanged?.Invoke(this, EventArgs.Empty);
             }
             break;
         case State.GameOver:
             break;
        }
    }

    public bool IsGamePlaying()
    {
        return _state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return _state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return _countdownToStartTimer;
    }
    public bool IsGameOverActive()
    {
        return _state == State.GameOver;
    }

    public float GetPLayingTimerNormalized()
    {
        return 1 - (_gamePlayingTimer / _gamePlayingTimerMax);
    }
}
