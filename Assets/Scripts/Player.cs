using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs: EventArgs
    {
        public BaseCounter SelectedCounter;
    }

    [SerializeField] private float _movementSpeed = 0;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private LayerMask _countersLayerMask;
    [SerializeField] private Transform _kitchenObjectHoldPoint = null;

    private bool _isWalking = false;
    private Vector3 _lastInteractDirection = new Vector3();
    private BaseCounter _selectedCounter = null;
    private KitchenObject _kitchenObject = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is more than one player");
        }
        Instance = this;
    }
    private void Start()
    {
        _gameInput.OnInteractAction += GameInputOnInteraction;
    }
    
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleMovement()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();

        Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y);

        float playerRadius = 0.7f;
        float playerHeight = 2f;
        float movementDistance = _movementSpeed * Time.deltaTime;

        bool canMove = !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius, movementDirection, movementDistance
        );

        if (!canMove)
        {
            //  Cannot move towards movementDirection
            //  Attempt to move only on X
            Vector3 movementX = new Vector3(movementDirection.x, 0, 0).normalized;
            canMove = movementDirection.x != 0 && !Physics.CapsuleCast(
                transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius, movementX, movementDistance
            );
            if (canMove)
            {
                //  Can move only on x
                movementDirection = movementX;
            }
            else
            {
                Vector3 movementZ = new Vector3(0, 0, movementDirection.z).normalized;
                canMove = movementDirection.z != 0 && !Physics.CapsuleCast(
                    transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius, movementZ, movementDistance
                );
                if (canMove)
                {
                    movementDirection = movementZ;
                }
                else
                {
                    //  Cannot move at all
                }
            }
        }

        if (canMove)
        {
            transform.position += movementDirection * movementDistance;
        }

        _isWalking = movementDirection != Vector3.zero;
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotationSpeed);
    }
    public bool IsWalking()
    {
        return _isWalking;
    }
    private void HandleInteractions()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y);

        if (movementDirection != Vector3.zero) _lastInteractDirection = movementDirection;

        var interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit raycastHit, interactDistance, _countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != _selectedCounter)
                {
                    _selectedCounter = baseCounter;
                    SetSelectedCounter();
                }
            }
            else
            {
                _selectedCounter = null;
                SetSelectedCounter();
            }
        }
        else
        {
            _selectedCounter = null;
            SetSelectedCounter();
        }
    }

    private void SetSelectedCounter()
    {
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            SelectedCounter = _selectedCounter
        });
    }

    private void GameInputOnInteraction(object sender, EventArgs e)
    {
        if (_selectedCounter != null)
        {
            _selectedCounter.Interact(this);
        }
    }


    public Transform GetKitchenObjectFollowTransform()
    {
        return _kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
