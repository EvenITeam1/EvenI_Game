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
    public bool IsJumpPressdMobile;
    public bool IsJumpUnHoldMobile;
    public bool IsAirHoldPressedMobile;
    public float moveAmount;

    private float movementInput;
    private bool jumpInput;
    private bool jumpUnhold;
    private bool airHold;
    PlayerControll inputActions;

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControll();
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
                jumpUnhold = !_input.ReadValueAsButton();
            };
            inputActions.TwoDimensions.Jump.performed += (_input) =>
            {
                jumpInput = _input.ReadValueAsButton();
                jumpUnhold = !_input.ReadValueAsButton();
            };
            inputActions.TwoDimensions.Jump.canceled += (_input) =>
            {
                jumpInput = _input.ReadValueAsButton();
                jumpUnhold = !_input.ReadValueAsButton();
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
    public void TickInput(float _delta) { 
        MoveInput(_delta);
        JumpInput();
        HoldInput();
    }
    public void MoveInput(float _delta)
    {
        Horizontal = movementInput;
        moveAmount = Mathf.Clamp01(Mathf.Abs(Horizontal));
    }
    public void JumpInput(){
        IsJumpPressd = jumpInput;
        IsJumpUnHold = jumpUnhold;
    }
    public void HoldInput(){
        IsAirHoldPressed = airHold;
    }
    public void JumpInputMobile(bool _input){
        IsJumpPressdMobile = _input;
    }
    public void HoldInputMobile(bool _input){
        IsAirHoldPressedMobile = _input;
    }
    public void JumpUnHoldMobild(bool _input){
        IsJumpUnHoldMobile = _input;
    }

    public bool CheckJumpInput(){
        bool res = (IsJumpPressd || IsJumpPressdMobile);
        return res;
    }

    public bool CheckHoldInput(){
        bool res = (IsAirHoldPressed || IsAirHoldPressedMobile);
        return res;
    }
}
