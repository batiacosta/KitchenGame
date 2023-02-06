using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 0;
    [SerializeField] private GameInput _gameInput;

    private bool _isWalking = false;
    private void Update()
    {

        Vector2 inputVector = _gameInput.GetMovementVectorNormalized();
        inputVector = inputVector.normalized;

        Vector3 movementDirection = new Vector3(inputVector.x, 0, inputVector.y);

        _isWalking = movementDirection != Vector3.zero;
        float rotationSpeed = 10f;
        transform.position += movementDirection * _movementSpeed * Time.deltaTime;
        transform.forward = Vector3.Slerp(transform.forward, movementDirection, Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking()
    {
        return _isWalking;
    }
}
