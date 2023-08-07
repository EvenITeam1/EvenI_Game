using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private EventTrigger JumpButtonUI;
    private EventTrigger HoldButtonUI;
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
        JumpButtonUI = GameObject.Find("JumpButtonUI").GetComponent<EventTrigger>();
        HoldButtonUI = GameObject.Find("HoldButtonUI").GetComponent<EventTrigger>();
        
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

        EventTrigger.Entry jumpentry_PointerDown = new EventTrigger.Entry();
        EventTrigger.Entry jumpentry_PointerUp = new EventTrigger.Entry();
        jumpentry_PointerDown.eventID = EventTriggerType.PointerDown;
        jumpentry_PointerUp.eventID = EventTriggerType.PointerUp;
        
        jumpentry_PointerDown.callback.AddListener((data) => {JumpInputMobile(true);});
        jumpentry_PointerDown.callback.AddListener((data) => {JumpUnHoldMobild(false);});


        jumpentry_PointerUp.callback.AddListener((data) => {JumpInputMobile(false);});
        jumpentry_PointerUp.callback.AddListener((data) => {JumpUnHoldMobild(true);});

        JumpButtonUI.triggers.Add(jumpentry_PointerDown);
        JumpButtonUI.triggers.Add(jumpentry_PointerUp);

        EventTrigger.Entry holdentry_PointerDown = new EventTrigger.Entry();
        EventTrigger.Entry holdentry_PointerUp = new EventTrigger.Entry();
        holdentry_PointerDown.eventID = EventTriggerType.PointerDown;
        holdentry_PointerUp.eventID = EventTriggerType.PointerUp;
        holdentry_PointerDown.callback.AddListener((data) => {HoldInputMobile(true);});
        holdentry_PointerUp.callback.AddListener((data) => {HoldInputMobile(false);});

        HoldButtonUI.triggers.Add(holdentry_PointerDown);
        HoldButtonUI.triggers.Add(holdentry_PointerUp);
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
