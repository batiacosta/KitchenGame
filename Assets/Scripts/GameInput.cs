using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{

    public static GameInput Instance;
    public event EventHandler OnInteractAction; //   Action OnInteractPerformed
    public event EventHandler OnInteractAlternAction;
    public event EventHandler OnPauseAction;
    
    
    private PlayerInputActions _playerInputActions;
    
    private void AlternateInteractPerfomed(InputAction.CallbackContext obj)
    {
        OnInteractAlternAction?.Invoke(this, EventArgs.Empty);
    }
    private void InteractPerformed(InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Awake()
    {
        Instance = this;
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Enable();
        _playerInputActions.Player.Interact.performed += InteractPerformed;
        _playerInputActions.Player.InteractAlternate.performed += AlternateInteractPerfomed;
        _playerInputActions.Player.Pause.performed += PausePerformed;
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Interact.performed -= InteractPerformed;
        _playerInputActions.Player.InteractAlternate.performed -= AlternateInteractPerfomed;
        _playerInputActions.Player.Pause.performed -= PausePerformed;
        _playerInputActions.Dispose();
    }

    private void PausePerformed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = _playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
