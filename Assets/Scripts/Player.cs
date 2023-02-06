using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 0;

    private bool _isWalking = false;
    private void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = + 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = - 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = - 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = + 1;
        }

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
