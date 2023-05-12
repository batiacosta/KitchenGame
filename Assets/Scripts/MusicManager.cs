using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    
    private AudioSource _audioSource;
    private float _volume = 0.3f;
    
    private const string PlayerPrefsMusicVolume = "MusicVolume";

    private void Awake()
    {
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
        _volume = PlayerPrefs.GetFloat(PlayerPrefsMusicVolume);
        _audioSource.volume = _volume;
    }

    public void ChangeVolume()
    {
        _volume += 0.1f;
        if (_volume > 1f)
        {
            _volume = 0;
        }

        _audioSource.volume = _volume;
        PlayerPrefs.SetFloat(PlayerPrefsMusicVolume, _volume);
        PlayerPrefs.Save();
    }

    

    public float GetVolume()
    {
        return _volume;
    }
}
