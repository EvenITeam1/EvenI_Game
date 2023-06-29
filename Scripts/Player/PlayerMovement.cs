using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState {Jump = 0, Hold}

namespace TwoDimensions
{
    public class PlayerMovement : MonoBehaviour
    {
        public float    horizontal;
        public float    speed = 8f;
        public float    jumpingPower = 16f;
        public int      maxJumpCount   = 3;
        public int      jumpCount;
        public bool     IsActivatedOnce = false;
        public bool     isFacingRight = true;

        public bool     isJumping;
        public bool     isAiring;
        public bool     isAirHoldable = true;

        public  float   coyoteTime = 0.2f;
        public  float   coyoteTimeCounter;
        
        public  float   jumpBufferTime = 0.2f;
        public  float   jumpBufferCounter;

        private PlayerState playerState;
        private float originGravityScale;

        [SerializeField] private Collider2D         cd;
        public Collider2D GetCollider() {return this.cd;}
        [SerializeField] private InputHandler       inputHandler;
        [SerializeField] private Rigidbody2D        rb;
        [SerializeField] private Transform          groundCheckerTransform;
        [SerializeField] private LayerMask          groundLayer;
        [SerializeField] private SpriteRenderer     spriteRenderer;
        [SerializeField] private Animator           animator;
        
        private Coroutine jumpCooldownCoroutine;
        private bool IsJumpCooldownCoroutineRunnting = false;

/*********************************************************************************/

        private void Awake() {
            jumpCount = maxJumpCount ;

            inputHandler ??= GetComponent<InputHandler>();
            rb  ??= GetComponent<Rigidbody2D>();
            cd  ??= GetComponent<Collider2D>();
            spriteRenderer ??= GetComponent<SpriteRenderer>();
            animator ??= GetComponent<Animator>();
            if(groundCheckerTransform == null){throw new System.Exception("자식 오브젝트에 있는 GroundCheck GameObject 가저올것");}
            originGravityScale = rb.gravityScale;
        }
        private void Start() {
            FlipSprite(1);
        }
        private void Update()
        {
            inputHandler.TickInput(0);
            Move();
            Jump();
            Animations();
        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
/*********************************************************************************/

#region Visual
        private void FlipSprite(float _horizontal){
            if      (_horizontal < 0) {spriteRenderer.flipX = false; }
            else if (0 < _horizontal) {spriteRenderer.flipX = true; }
        }
        private void Animations() {
            animator.SetFloat("MoveSpeed"   , Mathf.Abs(rb.velocity.x));
            animator.SetBool("Ascend"       , rb.velocity.y > 0.01f);
            animator.SetBool("Falling"      , rb.velocity.y < -0.01f);
        }
#endregion 

/*********************************************************************************/

#region Move
        private void Move(){ 
            //horizontal = inputHandler.Horizontal; //키보드 인풋 받기
            horizontal = 1;
            FlipSprite(1);
        }
#endregion

/*********************************************************************************/

#region Jump
        private void Jump(){
            HandleHoldUp(inputHandler.IsJumpPressd);

            if(inputHandler.IsAirHold && isAirHoldable && !IsGrounded()){
                rb.gravityScale = 0;
                rb.velocity = Vector2.right * rb.velocity.x;
                isAiring = true;
                return;
            }
            if(!inputHandler.IsAirHold){
                rb.gravityScale = originGravityScale;
                isAiring = false;
            }
//          HandleCoyote(IsGrounded());
//          HandleInputBuffer(inputHandler.IsJumpPressd);

//          if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping && jumpCount > 0)
            if (inputHandler.IsJumpPressd && !isJumping && jumpCount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                if(!IsActivatedOnce){IsActivatedOnce = true; jumpCount--;}
                jumpBufferCounter = 0f;
                ActivateJumpCool();
            }

            if (!inputHandler.IsJumpPressd && rb.velocity.y > 0f) {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                coyoteTimeCounter = 0f;
            }
        }

        public void RestoreJumpCount(){jumpCount = maxJumpCount;}

        public void HandleHoldUp(bool _isPressed){
            if(_isPressed == false) {
                IsActivatedOnce = false;
                isJumping = false;
            }
        }

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheckerTransform.position, 1, groundLayer);
        }

        private void ActivateJumpCool(){
            if(IsJumpCooldownCoroutineRunnting == false){
                jumpCooldownCoroutine = StartCoroutine(JumpCooldown());
            }
            else {
                StopCoroutine(jumpCooldownCoroutine);
                jumpCooldownCoroutine = StartCoroutine(JumpCooldown());
            }
        }

        private IEnumerator JumpCooldown()
        {
            isJumping = true;

            yield return new WaitUntil(() => {
                return Physics2D.OverlapCircle(groundCheckerTransform.position, 1, groundLayer) && GlobalFunction.GetIsFloatEqual(rb.velocity.y, 0.001f);
            });
            isJumping = false;
            RestoreJumpCount();
        }

        private void HandleCoyote(bool _isGrounded){ //IsGrounded()
            if (_isGrounded) { coyoteTimeCounter = coyoteTime; }
            else {coyoteTimeCounter -= Time.deltaTime;}
        }

        private void HandleInputBuffer(bool _isPressed){//
            if (_isPressed) { jumpBufferCounter = jumpBufferTime; }
            else{jumpBufferCounter -= Time.deltaTime;}

        }
#endregion

/*********************************************************************************/

#region State
        private void ChangeState(PlayerState _newState){
            StopCoroutine(playerState.ToString());
            playerState = _newState;
            StartCoroutine(playerState.ToString());
        }
#endregion
    }
}