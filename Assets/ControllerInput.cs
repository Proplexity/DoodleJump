using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{
    InputActions _input;

    public Vector2 moveInput { get; private set; }

    private void OnEnable()
    {
        _input = new InputActions();
        _input.Enable();

        _input.PlayerControls.Move.performed += SetMove;
        _input.PlayerControls.Move.canceled += SetMove;

    }

    private void OnDisable()
    {
        _input.PlayerControls.Move.performed -= SetMove;
        _input.PlayerControls.Move.canceled -= SetMove;

        _input.Disable();

    }

    private void SetMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }
}
