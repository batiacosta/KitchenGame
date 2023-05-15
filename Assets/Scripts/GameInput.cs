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

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        Interact_Alternate,
        Pause
    }
    
    
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

    public string GetBindingText(Binding binding)
    {
        string bindingString;
        switch (binding)
        {
            case Binding.Interact:
                bindingString = _playerInputActions.Player.Interact.bindings[0].ToDisplayString();
                break;
            case Binding.Interact_Alternate:
                bindingString = _playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
                break;
            case Binding.Pause:
                bindingString = _playerInputActions.Player.Pause.bindings[0].ToDisplayString();
                break;
            case Binding.Move_Up:
                bindingString = _playerInputActions.Player.Move.bindings[1].ToDisplayString();
                break;
            case Binding.Move_Down:
                bindingString = _playerInputActions.Player.Move.bindings[2].ToDisplayString();
                break;
            case Binding.Move_Left:
                bindingString = _playerInputActions.Player.Move.bindings[3].ToDisplayString();
                break;
            case Binding.Move_Right:
                bindingString = _playerInputActions.Player.Move.bindings[4].ToDisplayString();
                break;
            
            default:
                bindingString = "Not assigned yet";
                break;
        }

        return bindingString;
    }

    public void RebindBinding(Binding binding, Action OnActionRebound)
    {
        _playerInputActions.Disable();

        InputAction inputAction;
        int bindingIndex;
        switch (binding)
        {
            case Binding.Move_Up:
                inputAction = _playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = _playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = _playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = _playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = _playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.Interact_Alternate:
                inputAction = _playerInputActions.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = _playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
            default:
                bindingIndex = 0;
                inputAction = _playerInputActions.Player.Move;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(
            callback =>
            {
                _playerInputActions.Player.Enable();
                callback.Dispose();
                OnActionRebound();
            }
        ).Start();
    }
}
