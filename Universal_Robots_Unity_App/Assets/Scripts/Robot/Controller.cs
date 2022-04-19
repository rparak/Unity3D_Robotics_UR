using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Controller : MonoBehaviour
{
    
    public float speed = 0.05f;
    public float time = 0.05f;
    public float acceleration = 0.1f;

    private Vector3 movement;
    private Vector3 rotation;

    [Space]
    public InputActionReference movementAction;
    public InputActionReference altiduteAction;
    public InputActionReference rotationAction;


    private void Update()
    {
        if (!Robot.Connection.IsActive) return;

        Movement();
        Altidute();
        Rotation();

        if (movement == Vector3.zero && rotation == Vector3.zero) return;

        Robot.CMD.SpeedL(movement, rotation, acceleration, time);
        movement = rotation = Vector3.zero;
    }


    private void Movement()
    {
        Vector2 inputV = movementAction.action.ReadValue<Vector2>();

        movement.x += inputV.x * speed;
        movement.z += inputV.y * speed;
    }

    private void Altidute()
    {
        Vector2 inputV = altiduteAction.action.ReadValue<Vector2>();

        movement.y += inputV.x * speed;
        rotation.z += inputV.y * speed;
    }

    private void Rotation()
    {
        Vector2 inputV = rotationAction.action.ReadValue<Vector2>();

        rotation.x += inputV.x * speed;
        rotation.y += inputV.y * speed;
    }
}
