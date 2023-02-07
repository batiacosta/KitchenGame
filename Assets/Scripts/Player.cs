using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 0;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private LayerMask _countersLayerMask;

    private bool _isWalking = false;
    private Vector3 _lastInteractDirection = new Vector3();
    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y);

        if (movementDirection != Vector3.zero) _lastInteractDirection = movementDirection;

        var interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractDirection, out RaycastHit raycastHit, interactDistance, _countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //  Has ClearCounter
                clearCounter.Interact();
            }

            
        }
        else
        {
            Debug.Log($"-");
        }
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
            canMove = !Physics.CapsuleCast(
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
                canMove = !Physics.CapsuleCast(
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
}
