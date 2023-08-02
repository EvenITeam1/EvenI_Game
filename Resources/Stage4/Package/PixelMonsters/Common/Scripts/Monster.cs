using System;
using System.Collections;
using UnityEngine;

namespace Assets.PixelMonsters.Common.Scripts
{
    /// <summary>
    /// The main character script.
    /// </summary>
    public class Monster : MonoBehaviour
    {
        public Animator Animator;
        public CharacterController Controller;
        public SpriteRenderer Body;

        private static Material DefaultMaterial;
        private static Material BlinkMaterial;

        public void Blink()
        {
            if (DefaultMaterial == null) DefaultMaterial = Body.sharedMaterial;
            if (BlinkMaterial == null) BlinkMaterial = new Material(Shader.Find("GUI/Text Shader"));

            StartCoroutine(BlinkCoroutine());
        }

        private IEnumerator BlinkCoroutine()
        {
            Body.material = BlinkMaterial;

            yield return new WaitForSeconds(0.1f);

            Body.material = DefaultMaterial;
        }

        public void SetState(AnimationState state)
        {
            foreach (var variable in new[] { "Idle", "Ready", "Walking", "Running", "Jumping", "Dead" })
            {
                Animator.SetBool(variable, false);
            }

            switch (state)
            {
                case AnimationState.Idle: Animator.SetBool("Idle", true); break;
                case AnimationState.Ready: Animator.SetBool("Ready", true); break;
                case AnimationState.Walking: Animator.SetBool("Walking", true); break;
                case AnimationState.Running: Animator.SetBool("Running", true); break;
                case AnimationState.Jumping: Animator.SetBool("Jumping", true); break;
                case AnimationState.Dead: Animator.SetBool("Dead", true); break;
                default: throw new NotSupportedException();
            }

            //Debug.Log("SetState: " + state);
        }

        public AnimationState GetState()
        {
            if (Animator.GetBool("Idle")) return AnimationState.Idle;
            if (Animator.GetBool("Ready")) return AnimationState.Ready;
            if (Animator.GetBool("Walking")) return AnimationState.Walking;
            if (Animator.GetBool("Running")) return AnimationState.Running;
            if (Animator.GetBool("Jumping")) return AnimationState.Jumping;
            if (Animator.GetBool("Dead")) return AnimationState.Dead;

            return AnimationState.Ready;
        }
    }
}