using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public EventHandler OnInteractAction; //   Action OnInteractPerformed
    public EventHandler OnInteractAlternAction;
    private PlayerInputActions _playerInputActions;
    
    private void AlternateInteractPerfomed(InputAction.CallbackContext obj)
    {
        OnInteractAlternAction?.Invoke(this, EventArgs.Empty);
    }
    private void InteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);    //  OnInteractAction?.Invoke(); if using Action
    }

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Interact.performed += InteractPerformed;
        _playerInputActions.Player.InteractAlternate.performed += AlternateInteractPerfomed;
    }
    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
