using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TwoDimensions {    
    public class InputHandler : MonoBehaviour {
        public float    Horizontal;
        public bool    IsJumpPressd;
        public float    moveAmount;
        private float   movementInput;
        private bool   jumpInput;
        RunnerControll inputActions;

        private void OnEnable(){
            if(inputActions == null) {
                inputActions = new RunnerControll();
                inputActions.TwoDimensions.Movement.performed += (_input) => {
                    movementInput = _input.ReadValue<float>();
                };
                inputActions.TwoDimensions.Jump.started += (_input) => {
                    jumpInput = _input.ReadValueAsButton();
                    Debug.Log(jumpInput);
                };
                inputActions.TwoDimensions.Movement.canceled += (_input) => {
                    movementInput = _input.ReadValue<float>();
                };
                inputActions.TwoDimensions.Jump.canceled += (_input) => {
                    jumpInput = _input.ReadValueAsButton();
                    Debug.Log(jumpInput);
                };
            }
            inputActions.Enable();
        }


        private void OnDisable(){inputActions.Disable();}
        public void TickInput(float _delta) {MoveInput(_delta);}
        public void MoveInput(float _delta) {
            Horizontal = movementInput;
            IsJumpPressd = jumpInput;
            moveAmount = Mathf.Clamp01(Mathf.Abs(Horizontal));
        }
    }
}
