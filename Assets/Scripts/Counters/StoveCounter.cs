using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State State;
    }
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    
    [SerializeField] private FryingRecipeSO[] _fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] _burningRecipeSOArray;

    private float _fryingTimer;
    private float _burningTimer;
    private FryingRecipeSO _fryingRecipeSO;
    private BurningRecipeSO _burningRecipeSO;
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    private State _state;

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (_state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    _fryingTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = _fryingTimer / _fryingRecipeSO.FryingTimerMax
                    });
                    if (_fryingTimer > _fryingRecipeSO.FryingTimerMax)
                    {
                        //  Fried
                        _fryingTimer = 0f;
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(_fryingRecipeSO.Output, this);
                        _state = State.Fried;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { State = _state });
                        _burningTimer = 0f;
                        _burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    }
                    break;
                case State.Fried:
                    _burningTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = _burningTimer / _burningRecipeSO.BurningTimerMax
                    });
                    if (_burningTimer > _burningRecipeSO.BurningTimerMax)
                    {
                        //  Fried
                        _fryingTimer = 0f;
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(_burningRecipeSO.Output, this);
                        _state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { State = _state });
                        _burningTimer = 0f;
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            ProgressNormalized = 0f
                        });
                    }
                    break;
                case State.Burned :
                    break;
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //  Player drops something that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    _fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    _state = State.Frying;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { State = _state });
                    _fryingTimer = 0f;
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = _fryingTimer / _fryingRecipeSO.FryingTimerMax
                    });
                }
            }
            else
            {
                //  Player has nothing, so nothing happens
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //  Player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                        _state = State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { State = _state });
                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            ProgressNormalized = 0f
                        });
                    }
                }
            }
            else
            {
                //  player picks up anything on top of this counter
                GetKitchenObject().SetKitchenObjectParent(player);
                _state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { State = _state });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    ProgressNormalized = 0f
                });
            }
            
        }
    }
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.Output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var fryingRecipeSO in _fryingRecipeSOArray)
        {
            if (fryingRecipeSO.Input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (var burningRecipeSO in _burningRecipeSOArray)
        {
            if (burningRecipeSO.Input == inputKitchenObjectSO)
            {
                return burningRecipeSO;
            }
        }
        return null;
    }
    public bool IsFried()
    {
        return _state == State.Fried;
    }
}
