// Copyright (c) 2017, Timothy Ned Atton.
// All rights reserved.
// nedmakesgames@gmail.com
// This code was written while streaming on twitch.tv/nedmakesgames
//
// This file is part of Raycast Platformer.
//
// Raycast Platformer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Raycast Platformer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Raycast Platformer.  If not, see <http://www.gnu.org/licenses/>.

using UnityEngine;
using System.Collections;
using System;

namespace PSO
{
    public class RaycastPlayer : MonoBehaviour
    {

        private enum JumpState
        {
            None = 0, Holding,
        }

        [SerializeField]
        private LayerMask platformMask;
        [SerializeField]
        private float parallelInsetLen = 0.2f;
        [SerializeField]
        private float perpendicularInsetLen = 0.2f;
        [SerializeField]
        private float groundTestLen = 0.05f;
        [SerializeField]
        private float gravity = 20f;
        [SerializeField]
        private float horizSpeedUpAccel = 20f;
        [SerializeField]
        private float horizSpeedDownAccel = 24f;
        [SerializeField]
        private float horizSnapSpeed = 1.5f;
        [SerializeField]
        private float horizMaxSpeed = 4f;
        [SerializeField]
        private float jumpInputLeewayPeriod = 0.25f;
        [SerializeField]
        private float jumpStartSpeed = 5f;
        [SerializeField]
        private float jumpMaxHoldPeriod = 0.25f;
        [SerializeField]
        private float jumpMinSpeed = 2f;
        [SerializeField]
        private int maxJumps = 2;

        private int jumpsLeft;

        public float AttackVelocityX = 0;
        public bool AntiGravity = false;

        public FacingDir Direction;
        public bool Attacking = false;

        [SerializeField]
        public Animator animator;
        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Transform SpriteHolder;

        private Vector2 velocity;

        private RaycastMoveDirection moveDown;
        private RaycastMoveDirection moveLeft;
        private RaycastMoveDirection moveRight;
        private RaycastMoveDirection moveUp;

        private RaycastCheckTouch groundDown;

        private Vector2 lastStandingOnPos;
        private Vector2 lastStandingOnVel;
        private Collider2D lastStandingOn;

        private float jumpStartTimer;
        private float jumpHoldTimer;
        private bool jumpInputDown;
        private JumpState jumpState;
        private bool lastGrounded;
        private bool grounded;

        [SerializeField]
        private BoxCollider2D boxCollider;
        private float halfColliderWidth;
        private float halfColliderHeight;

        public bool IsAlive = true;
        public bool Stunned = false;
        public bool Dying = false;

        private float InternalCooldown = 1.0f;
        private float CooldownTimer = 0;
        public bool OnCooldown = false;

        public void Init()
        {
            halfColliderWidth = boxCollider.bounds.extents.x;
            halfColliderHeight = boxCollider.bounds.extents.y;

            moveDown = new RaycastMoveDirection(new Vector2(-halfColliderWidth, -halfColliderHeight), new Vector2(halfColliderWidth, -halfColliderHeight), Vector2.down, platformMask,
                Vector2.right * parallelInsetLen, Vector2.up * perpendicularInsetLen);
            moveLeft = new RaycastMoveDirection(new Vector2(-halfColliderWidth, -halfColliderHeight), new Vector2(-halfColliderWidth, halfColliderHeight), Vector2.left, platformMask,
                Vector2.up * parallelInsetLen, Vector2.right * perpendicularInsetLen);
            moveUp = new RaycastMoveDirection(new Vector2(-halfColliderWidth, halfColliderHeight), new Vector2(halfColliderWidth, halfColliderHeight), Vector2.up, platformMask,
                Vector2.right * parallelInsetLen, Vector2.down * perpendicularInsetLen);
            moveRight = new RaycastMoveDirection(new Vector2(halfColliderWidth, -halfColliderHeight), new Vector2(halfColliderWidth, halfColliderHeight), Vector2.right, platformMask,
                Vector2.up * parallelInsetLen, Vector2.left * perpendicularInsetLen);

            groundDown = new RaycastCheckTouch(new Vector2(-halfColliderWidth, -halfColliderHeight), new Vector2(halfColliderWidth, -halfColliderHeight), Vector2.down, platformMask,
                Vector2.right * parallelInsetLen, Vector2.up * perpendicularInsetLen, groundTestLen);

            jumpsLeft = maxJumps;
        }

        public void TriggerCooldown()
        {
            OnCooldown = true;
        }

        private int GetSign(float v)
        {
            if (Mathf.Approximately(v, 0))
            {
                return 0;
            }
            else if (v > 0)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public void Tick()
        {
            if (!IsAlive)
                return;

            jumpStartTimer -= Time.deltaTime;
            bool jumpBtn = Input.GetButton("Jump");
            if (jumpBtn && jumpInputDown == false)
            {
                jumpStartTimer = jumpInputLeewayPeriod;
            }
            jumpInputDown = jumpBtn;

            Direction = SpriteHolder.localScale.x == -1 ? FacingDir.Left : FacingDir.Right;

            if (OnCooldown)
            {
                if (CooldownTimer >= InternalCooldown)
                {
                    CooldownTimer = 0;
                    OnCooldown = false;
                }
                else
                    CooldownTimer += Time.deltaTime;
            }
        }

        public void FixedTick()
        {
            Collider2D standingOn = groundDown.DoRaycast(transform.position);
            grounded = standingOn != null;
            if (grounded && lastGrounded == false)
            {
                //just landed
            }
            lastGrounded = grounded;

            if (grounded)
            {
                jumpsLeft = maxJumps;
            }

            switch (jumpState)
            {
                case JumpState.None:
                    if (jumpStartTimer > 0 && jumpsLeft > 0)
                    {
                        jumpsLeft--;
                        jumpStartTimer = 0;
                        jumpState = JumpState.Holding;
                        jumpHoldTimer = 0;
                        velocity.y = jumpStartSpeed;
                    }
                    break;
                case JumpState.Holding:
                    jumpHoldTimer += Time.deltaTime;
                    if (jumpInputDown == false || jumpHoldTimer >= jumpMaxHoldPeriod)
                    {
                        jumpState = JumpState.None;
                        velocity.y = Mathf.Lerp(jumpMinSpeed, jumpStartSpeed, jumpHoldTimer / jumpMaxHoldPeriod);
                    }
                    break;
            }

            float horizInput = Input.GetAxisRaw("Horizontal");
            int wantedDirection = GetSign(horizInput);
            int velocityDirection = GetSign(velocity.x);

            if (AttackVelocityX != 0)
            {
                velocity.x = AttackVelocityX;
            }
            else if ((Attacking && grounded) || Stunned || !IsAlive)
            {
                velocity.x = 0;
            }
            else if (wantedDirection != 0)
            {
                if (wantedDirection != velocityDirection)
                {
                    velocity.x = horizSnapSpeed * wantedDirection;
                }
                else
                {
                    velocity.x = Mathf.MoveTowards(velocity.x, horizMaxSpeed * wantedDirection, horizSpeedUpAccel * Time.deltaTime);
                }
            }
            else
            {
                velocity.x = Mathf.MoveTowards(velocity.x, 0, horizSpeedDownAccel * Time.deltaTime);
            }

            if (AntiGravity)
            {
                velocity.y = 0;
            }
            else if (jumpState == JumpState.None)
            {
                velocity.y -= gravity * Time.deltaTime;
            }

            Vector2 displacement = Vector2.zero;
            Vector2 wantedDispl = velocity * Time.deltaTime;

            if (standingOn != null)
            {
                if (lastStandingOn == standingOn)
                {
                    lastStandingOnVel = (Vector2)standingOn.transform.position - lastStandingOnPos;
                    wantedDispl += lastStandingOnVel;
                }
                else if (standingOn == null)
                {
                    velocity += lastStandingOnVel / Time.deltaTime;
                    wantedDispl += lastStandingOnVel;
                }
                lastStandingOnPos = standingOn.transform.position;
            }
            lastStandingOn = standingOn;

            if (wantedDispl.x > 0)
            {
                displacement.x = moveRight.DoRaycast(transform.position, wantedDispl.x);
            }
            else if (wantedDispl.x < 0)
            {
                displacement.x = -moveLeft.DoRaycast(transform.position, -wantedDispl.x);
            }
            if (wantedDispl.y > 0)
            {
                displacement.y = moveUp.DoRaycast(transform.position, wantedDispl.y);
            }
            else if (wantedDispl.y < 0)
            {
                displacement.y = -moveDown.DoRaycast(transform.position, -wantedDispl.y);
            }

            if (Mathf.Approximately(displacement.x, wantedDispl.x) == false)
            {
                velocity.x = 0;
            }
            if (Mathf.Approximately(displacement.y, wantedDispl.y) == false)
            {
                velocity.y = 0;
            }

            transform.Translate(displacement);

            if (wantedDirection != 0 && !Attacking && IsAlive)
            {
                SpriteHolder.localScale = new Vector3(wantedDirection < 0 ? -1 : 1, SpriteHolder.localScale.y);
            }
        }

        public void UpdateAnimatorBase()
        {
            animator.SetBool("Alive", IsAlive);
            animator.SetBool("Stunned", Stunned);
            animator.SetBool("Grounded", grounded);
            animator.SetBool("Attacking", Attacking);

            if (Attacking || !IsAlive || Stunned)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Walking", false);
            }
            else
            {
                animator.SetBool("Idle", velocity.x == 0 && grounded);
                animator.SetBool("Walking", velocity.x != 0 && grounded);
            }

            animator.SetFloat("xSpeed", velocity.x);

            if(maxJumps > 0)
                animator.SetFloat("ySpeed", velocity.y);
        }

        public void Die()
        {
            IsAlive = false;
            Dying = true;

            animator.SetBool("Alive", IsAlive);
            animator.SetBool("Dying", Dying);
        }

        public void Revive()
        {
            IsAlive = true;
            Dying = false;

            animator.SetBool("Alive", IsAlive);
            animator.SetBool("Dying", Dying);
        }

        public virtual void StopAttacks()
        {
            Attacking = false;
        }

        public virtual void SpawnAttackEffect(int attackIndex)
        {
            //for spawning attack effects
        }

        public virtual void StopAttackEffects()
        {
            //for stopping attack effects
        }

        public virtual void FinishedDying()
        {
            Dying = false;
            animator.SetBool("Dying", Dying);
        }

        public void ToggleStun()
        {
            if (Stunned)
            {
                FinishedStun();
            }
            else
            {
                Stun();
            }
        }

        public void Stun()
        {
            Stunned = true;
            animator.Play("Flinch");
            animator.SetBool("Stunned", Stunned);
            animator.SetBool("Idle", false);
            animator.SetBool("Walking", false);
            animator.SetFloat("xSpeed", velocity.x);
        }

        public void ToggleDazed()
        {
            Stunned = !Stunned;
            animator.SetBool("Stunned", Stunned);

            if(!Stunned)
            {
                animator.SetBool("Dazed", false);
            }
        }

        public void Dazed()
        {
            animator.SetBool("Dazed", true);
        }

        public virtual void FinishedStun()
        {
            Stunned = false;
            animator.SetBool("Stunned", Stunned);
        }

        public void ToggleDeath()
        {
            if (IsAlive)
            {
                Die();
            }
            else if (!Dying)
            {
                Revive();
            }
        }

        public void SetAttackVelocityX(float vel)
        {
            if (Direction == FacingDir.Right)
                AttackVelocityX = vel;
            else
                AttackVelocityX = -vel;
        }
    }

    public enum FacingDir
    {
        Left,
        Right
    }
}