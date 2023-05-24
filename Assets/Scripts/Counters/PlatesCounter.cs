using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned; 
    public event EventHandler OnPlateRemoved; 
    [SerializeField] private KitchenObjectSO _plateKitchenObjectSO;
    private float _spawnPlateTimer = 0;
    private const float SpawnPlateTimer = 4f;
    private int _platesSpawnedAmout = 0;
    private int _platesSpawnedAmountMax = 4;

    private void Update()
    {
        _spawnPlateTimer += Time.deltaTime;
        if (GameManager.Instance.IsGamePlaying() && _spawnPlateTimer > SpawnPlateTimer)
        {
            _spawnPlateTimer = 0f;
            if (_platesSpawnedAmout < _platesSpawnedAmountMax)
            {
                _platesSpawnedAmout++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //  Player is empty handed
            if (_platesSpawnedAmout > 0)
            {
                //  There is at least one plate here
                _platesSpawnedAmout--;
                KitchenObject.SpawnKitchenObject(_plateKitchenObjectSO, player);//  Spawn a plate on player
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
