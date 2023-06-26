using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TwoDimensions
{
    public class Dog : MonoBehaviour
    {
        public float    horizontal;
        public float    speed = 8f;
        public float    jumpingPower = 16f;
        public bool     isFacingRight = true;

        public bool     isJumping;

        public  float   coyoteTime = 0.2f;
        public  float   coyoteTimeCounter;
        
        public  float   jumpBufferTime = 0.2f;
        public  float   jumpBufferCounter;

        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private LayerMask groundLayer;

        private void Update()
        {
            inputHandler.TickInput(0);
            Move();
            Jump();
        }

        private void Move(){
            horizontal = inputHandler.Horizontal;
        }

        private void Jump(){
            if (IsGrounded())
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            if (inputHandler.IsJumpPressd)
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);

                jumpBufferCounter = 0f;

                StartCoroutine(JumpCooldown());
            }

            if (!inputHandler.IsJumpPressd && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);

                coyoteTimeCounter = 0f;
            }
        }

        private void FixedUpdate()
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }

        private bool IsGrounded()
        {
            return Physics2D.OverlapCircle(groundCheck.position, 1, groundLayer);
        }
        private IEnumerator JumpCooldown()
        {
            isJumping = true;
            yield return new WaitUntil(() => {
                return Physics2D.OverlapCircle(groundCheck.position, 1, groundLayer) && GlobalFunction.GetIsFloatEqual(rb.velocity.y, 0.001f);
            });
            isJumping = false;
        }

    }
}