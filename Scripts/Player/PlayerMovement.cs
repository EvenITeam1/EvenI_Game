using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoDimensions
{
    public class PlayerMovement : MonoBehaviour
    {
        public float horizontal;
        public float speed = 8f;
        public float jumpingPower = 16f;
        public int maxJumpCount = 3;
        public int jumpCount;
        public bool IsActivatedOnce = false;
        public bool isFacingRight = true;

        public bool isJumping;

        public float coyoteTime = 0.2f;
        public float coyoteTimeCounter;

        public float jumpBufferTime = 0.2f;
        public float jumpBufferCounter;

        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform groundCheckerTransform;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;

        private Coroutine jumpCooldownCoroutine;
        private bool IsJumpCooldownCoroutineRunnting = false;

        /////////////////////////////////////////////////////////////////////////////////

        #region Basic
        private void Awake()
        {
            jumpCount = maxJumpCount;

            inputHandler ??= GetComponent<InputHandler>();
            rb ??= GetComponent<Rigidbody2D>();
            spriteRenderer ??= GetComponent<SpriteRenderer>();
            animator ??= GetComponent<Animator>();
            if (groundCheckerTransform == null) { throw new System.Exception("자식 오브젝트에 있는 GroundCheck GameObject 가저올것"); }
        }
        private void Update()
        {
            inputHandler.TickInput(0);
            Move();
            Jump();
        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        
        #endregion

        /////////////////////////////////////////////////////////////////////////////////

        #region Visual
        private void FlipSprite(float _horizontal)
        {
            if (_horizontal < 0) { spriteRenderer.flipX = false; }
            else if (0 < _horizontal) { spriteRenderer.flipX = true; }
        }
        #endregion
        
        ///////////////////////////////////////////////////////////////////////////////// 

        #region Move
        private void Move()
        {
            horizontal = inputHandler.Horizontal;
            FlipSprite(horizontal);
            animator.SetFloat("MoveSpeed", Mathf.Abs(rb.velocity.x));
        }
        #endregion
        
        /////////////////////////////////////////////////////////////////////////////////

        #region Jump
        private void Jump()
        {
            PlayerShoot shootScript = GetComponent<PlayerShoot>();//bullet 발사 관련 스크립트 가져오기
            animator.SetBool("Ascend", isJumping);
            HandleHoldUp(inputHandler.IsJumpPressd);
            //            HandleCoyote(IsGrounded());
            //            HandleInputBuffer(inputHandler.IsJumpPressd);

            //            if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping && jumpCount > 0)
            if (inputHandler.IsJumpPressd && !isJumping && jumpCount > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                if (!IsActivatedOnce) { IsActivatedOnce = true; jumpCount--; }

                if (!shootScript) { throw new System.Exception("Player 오브젝트에 PlayerShoot 스크립트가 없습니다."); }//추가타 코드
                else { shootScript.fireJumpBullet(); }//추가타 코드

                jumpBufferCounter = 0f;
                ActivateJumpCool();
            }

            if (!inputHandler.IsJumpPressd && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                coyoteTimeCounter = 0f;
            }
        }

        public void RestoreJumpCount()
        {

        }

        public void HandleHoldUp(bool _isPressed)
        {
            if (_isPressed == false)
            {
                IsActivatedOnce = false;
                isJumping = false;
            }
        }

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheckerTransform.position, 1, groundLayer);
        }

        private void ActivateJumpCool()
        {
            if (IsJumpCooldownCoroutineRunnting == false)
            {
                jumpCooldownCoroutine = StartCoroutine(JumpCooldown());
            }
            else
            {
                StopCoroutine(jumpCooldownCoroutine);
                jumpCooldownCoroutine = StartCoroutine(JumpCooldown());
            }
        }

        private IEnumerator JumpCooldown()
        {
            isJumping = true;

            yield return new WaitUntil(() =>
            {
                return Physics2D.OverlapCircle(groundCheckerTransform.position, 1, groundLayer) && GlobalFunction.GetIsFloatEqual(rb.velocity.y, 0.001f);
            });
            isJumping = false;
            jumpCount = maxJumpCount;
        }

        private void HandleCoyote(bool _isGrounded)
        { //IsGrounded()
            if (_isGrounded) { coyoteTimeCounter = coyoteTime; }
            else { coyoteTimeCounter -= Time.deltaTime; }
        }

        private void HandleInputBuffer(bool _isPressed)
        {
            if (_isPressed) { jumpBufferCounter = jumpBufferTime; }
            else { jumpBufferCounter -= Time.deltaTime; }

        }
        #endregion
        
        /////////////////////////////////////////////////////////////////////////////////
    }
}