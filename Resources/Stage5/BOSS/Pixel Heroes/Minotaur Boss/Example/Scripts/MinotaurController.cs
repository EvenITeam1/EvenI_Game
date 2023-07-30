using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PSO
{
    public class MinotaurController : RaycastPlayer
    {
        // Charge
        public float ChargeSpeed = 7.0f;
        public float ChargeDuration = 1.0f;
        private bool ChargeStarted = false;
        private float ChargeTimer = 0;

        // Use this for initialization
        void Start()
        {
            Init();
        }

        // Update is called once per frame
        void Update()
        {
            Tick();

            if (OnCooldown)
                return;

            HandleChargeMovement();

            if (Input.GetKeyDown(KeyCode.Alpha1) && !Attacking && IsAlive)
            {
                PerformSwing();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) && !Attacking && IsAlive)
            {
                PerformBash();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) && !Attacking && IsAlive)
            {
                PerformStomp();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4) && !Attacking && IsAlive)
            {
                PerformCharge();
            }

            if (Input.GetKeyDown(KeyCode.Alpha5) && !Attacking && IsAlive)
            {
                ToggleDazed();
            }

            if (Input.GetKeyDown(KeyCode.Alpha6) && !Attacking && !Stunned)
            {
                ToggleDeath();
            }
        }

        private void HandleChargeMovement()
        {
            if (ChargeStarted)
            {
                if (ChargeTimer < ChargeDuration)
                {
                    ChargeTimer += Time.deltaTime;
                }
                else
                {
                    ChargeTimer = 0;
                    ChargeStarted = false;
                    StopAttacks();
                }
            }
        }

        private void PerformCharge()
        {
            Attacking = true;
            ChargeStarted = true;
            SetAttackVelocityX(ChargeSpeed);
            animator.SetBool("ChargeAttackStart", true);
        }

        private void PerformStomp()
        {
            Attacking = true;
            animator.SetBool("StompAttackStart", true);
        }

        private void PerformBash()
        {
            Attacking = true;
            animator.SetBool("BashAttackStart", true);
        }

        private void PerformSwing()
        {
            Attacking = true;
            animator.SetBool("SwingAttackStart", true);
        }

        void FixedUpdate()
        {
            FixedTick();
            UpdateAnimatorBase();
        }

        public override void StopAttacks()
        {
            base.StopAttacks();
            animator.SetBool("SwingAttackStart", false);
            animator.SetBool("BashAttackStart", false);
            animator.SetBool("ChargeAttackStart", false);
            animator.SetBool("StompAttackStart", false);
            SetAttackVelocityX(0);
        }
    }
}