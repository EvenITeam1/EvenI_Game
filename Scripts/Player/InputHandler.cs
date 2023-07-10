using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public float Horizontal;
    public bool IsJumpPressd;
    public bool IsJumpUnHold;
    public bool IsAirHoldPressed;
    public float moveAmount;

    private float movementInput;
    private bool JumpUnHold;
    private bool jumpInput;
    private bool airHold;
    RunnerControll inputActions;

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new RunnerControll();
            inputActions.TwoDimensions.Movement.performed += (_input) =>
            {
                movementInput = _input.ReadValue<float>();
            };
            inputActions.TwoDimensions.Movement.canceled += (_input) =>
            {
                movementInput = _input.ReadValue<float>();
            };
            inputActions.TwoDimensions.Jump.started += (_input) =>
            {
                jumpInput = _input.ReadValueAsButton();
                JumpUnHold = !_input.ReadValueAsButton();
            };
            inputActions.TwoDimensions.Jump.performed += (_input) =>
            {
                jumpInput = _input.ReadValueAsButton();
                JumpUnHold = !_input.ReadValueAsButton();
            };
            inputActions.TwoDimensions.Jump.canceled += (_input) =>
            {
                jumpInput = _input.ReadValueAsButton();
                JumpUnHold = !_input.ReadValueAsButton();
            };
            inputActions.TwoDimensions.Hold.performed += (_input) =>
            {
                airHold = _input.ReadValueAsButton();
            };
            inputActions.TwoDimensions.Hold.canceled += (_input) =>
            {
                airHold = _input.ReadValueAsButton();
            };
        }
        inputActions.Enable();
    }


    private void OnDisable() { inputActions.Disable(); }
    public void TickInput(float _delta) { MoveInput(_delta); }
    public void MoveInput(float _delta)
    {
        Horizontal = movementInput;
        IsJumpPressd = jumpInput;
        IsJumpUnHold = JumpUnHold;
        IsAirHoldPressed = airHold;
        moveAmount = Mathf.Clamp01(Mathf.Abs(Horizontal));
    }
}
