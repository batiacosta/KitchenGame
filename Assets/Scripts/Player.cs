using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 0;
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

        transform.Translate(movementDirection * _movementSpeed * Time.deltaTime);
        Debug.Log($"{inputVector}");
    }
}
