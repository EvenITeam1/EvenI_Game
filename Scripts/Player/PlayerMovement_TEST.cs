using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoDimensions
{
    public class PlayerMovement_TEST : MonoBehaviour
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

        public  float   coyoteTime = 0.2f;
        public  float   coyoteTimeCounter;
        
        public  float   jumpBufferTime = 0.2f;
        public  float   jumpBufferCounter;

        [SerializeField] private InputHandler       inputHandler;
        [SerializeField] private Rigidbody2D        rb;
        [SerializeField] private Transform          groundCheckerTransform;
        [SerializeField] private LayerMask          groundLayer;
        [SerializeField] private SpriteRenderer     spriteRenderer;
        [SerializeField] private Animator           animator;
        
        private Coroutine jumpCooldownCoroutine;
        private Coroutine jumpHoldingCoroutine;
        private bool IsJumpCooldownCoroutineRunnting = false;

/*********************************************************************************/

        private void Awake() {
            jumpCount = maxJumpCount ;

            inputHandler ??= GetComponent<InputHandler>();
            rb  ??= GetComponent<Rigidbody2D>();
            spriteRenderer ??= GetComponent<SpriteRenderer>();
            animator ??= GetComponent<Animator>();
            if(groundCheckerTransform == null){throw new System.Exception("자식 오브젝트에 있는 GroundCheck GameObject 가저올것");}
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
            animator.SetBool("Ascend", isJumping);
//            HandleHoldUp(inputHandler.IsJumpPressd);

//          HandleCoyote(IsGrounded());
//          HandleInputBuffer(inputHandler.IsJumpPressd);
//          if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping && jumpCount > 0)
            
            if (inputHandler.IsJumpPressd && !isJumping && jumpCount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                if(!IsActivatedOnce){IsActivatedOnce = true; jumpCount--;}
                jumpBufferCounter = 0f;
                ActivateCoroutine(out jumpCooldownCoroutine, JumpCooldown(), IsJumpCooldownCoroutineRunnting);
            }

            if (!inputHandler.IsJumpPressd && rb.velocity.y > 0f) {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                coyoteTimeCounter = 0f;
            }
        }


        public void RestoreJumpCount(){

        }

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

        private void ActivateCoroutine(out Coroutine coroutine, IEnumerator _enumerator, bool _flag){
            if(_flag == false){
                coroutine = StartCoroutine(_enumerator);
            }
            else {
                StopCoroutine(jumpCooldownCoroutine);
                coroutine = StartCoroutine(_enumerator);
            }
        }

        private IEnumerator JumpCooldown()
        {
            isJumping = true;

            yield return new WaitUntil(() => {
                return Physics2D.OverlapCircle(groundCheckerTransform.position, 1, groundLayer) && GlobalFunction.GetIsFloatEqual(rb.velocity.y, 0.001f);
            });
            isJumping = false;
            jumpCount = maxJumpCount;
        }

        private void HandleCoyote(bool _isGrounded){ //IsGrounded()
            if (_isGrounded) { coyoteTimeCounter = coyoteTime; }
            else {coyoteTimeCounter -= Time.deltaTime;}
        }

        private void HandleInputBuffer(bool _isPressed){//
            if (_isPressed) { jumpBufferCounter = jumpBufferTime; }
            else{jumpBufferCounter -= Time.deltaTime;}

        }

        private IEnumerator JumpHolding(){
            rb.velocity = Vector2.right * rb.velocity.x;
            isAiring = true;
            yield return new WaitUntil(() => {return !inputHandler.IsAirHold;});
            isAiring = false;
        }
#endregion

/*********************************************************************************/
    }
}