using System;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player _player;
    private float _footstepTimer = 0.1f;
    private float footstepTimerMax = 0.1f;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        _footstepTimer -= Time.deltaTime;
        if (_footstepTimer < 0f)
        {
            _footstepTimer = footstepTimerMax;
            if (_player.IsWalking())
            {
                float volume = 2f;
                SoundManager.Instance.PlayFootstepsSound(_player.transform.position, volume);
            }
        }
    }
}
