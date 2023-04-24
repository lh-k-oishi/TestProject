using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput _input;
    Transform _transform;
    Move _move;

    private void Awake()
    {
        TryGetComponent(out _transform);
        TryGetComponent(out _input);
        TryGetComponent(out _move);
    }
    private void OnEnable()
    {
        _input.actions["Move"].performed += OnMove;
        _input.actions["Move"].canceled += OnMoveStop;
    }
    private void OnDisable()
    {
        _input.actions["Move"].performed -= OnMove;
        _input.actions["Move"].canceled -= OnMoveStop;
    }
    void OnMove(InputAction.CallbackContext obj)
    {
        var value = obj.ReadValue<Vector2>();
        var direction = new Vector3(value.x, 0, value.y);
        _move.SetDirection(direction);
    }
    void OnMoveStop(InputAction.CallbackContext obj)
    {
        Debug.Log("待機");
    }
}
