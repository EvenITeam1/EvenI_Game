using UnityEngine;

namespace Assets.PixelMonsters.Common.Scripts
{
    public class MonsterControls : MonoBehaviour
    {
        public Monster Monster;
        public CharacterController Controller; // https://docs.unity3d.com/ScriptReference/CharacterController.html
        public float RunSpeed = 1f;
        public float JumpSpeed = 3f;
        public float Gravity = -0.2f;
        public ParticleSystem MoveDust;
        public ParticleSystem JumpDust;

        private Vector3 _motion = Vector3.zero;
        private int _inputX, _inputY;
        private float _activityTime;

        public void Start()
        {
            Monster.SetState(AnimationState.Idle);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) Monster.Animator.SetTrigger("Attack");
            else if (Input.GetKeyDown(KeyCode.I)) { Monster.SetState(AnimationState.Idle); _activityTime = 0; }
            else if (Input.GetKeyDown(KeyCode.R)) { Monster.SetState(AnimationState.Ready); _activityTime = Time.time; }
            else if (Input.GetKeyDown(KeyCode.D)) Monster.SetState(AnimationState.Dead);
            else if (Input.GetKeyUp(KeyCode.L)) Monster.Blink();

            if (Controller.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    GetDown();
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    GetUp();
                }
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                _inputX = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                _inputX = 1;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _inputY = 1;

                if (Controller.isGrounded)
                {
                    JumpDust.Play(true);
                }
            }
        }

        public void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (Time.frameCount <= 1)
            {
                Controller.Move(new Vector3(0, Gravity) * Time.fixedDeltaTime);
                return;
            }

            var state = Monster.GetState();

            if (state == AnimationState.Dead)
            {
                if (_inputX == 0) return;

                Monster.SetState(AnimationState.Running);
            }

            if (_inputX != 0)
            {
                Turn(_inputX);
            }

            if (Controller.isGrounded)
            {
                if (state == AnimationState.Jumping)
                {
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        GetDown();
                    }
                    else
                    {
                        Monster.Animator.SetTrigger("Landed");
                        Monster.SetState(AnimationState.Ready);
                        JumpDust.Play(true);
                    }
                }

                _motion = new Vector3(RunSpeed * _inputX, JumpSpeed * _inputY);

                if (_inputX != 0 || _inputY != 0)
                {
                    if (_inputY > 0)
                    {
                        Monster.SetState(AnimationState.Jumping);
                    }
                    else
                    {
                        switch (state)
                        {
                            case AnimationState.Idle:
                            case AnimationState.Ready:
                                Monster.SetState(AnimationState.Running);
                                break;
                        }
                    }
                }
                else
                {
                    var targetState = Time.time - _activityTime > 5 ? AnimationState.Idle : AnimationState.Ready;

                    if (state != targetState)
                    {
                        Monster.SetState(targetState);
                    }
                }
            }
            else
            {
                _motion = new Vector3(RunSpeed * _inputX, _motion.y);
                Monster.SetState(AnimationState.Jumping);
            }

            _motion.y += Gravity;

            Controller.Move(_motion * Time.fixedDeltaTime);

            Monster.Animator.SetBool("Grounded", Controller.isGrounded);
            Monster.Animator.SetBool("Moving", Controller.isGrounded && _inputX != 0);

            if (_inputX != 0 && _inputY != 0 || Monster.Animator.GetBool("Action"))
            {
                _activityTime = Time.time;
            }

            _inputX = _inputY = 0;

            if (Controller.isGrounded && !Mathf.Approximately(Controller.velocity.x, 0))
            {
                var velocity = MoveDust.velocityOverLifetime;

                velocity.xMultiplier = 0.2f * -Mathf.Sign(Controller.velocity.x);

                if (!MoveDust.isPlaying)
                {
                    MoveDust.Play();
                }
            }
            else
            {
                MoveDust.Stop();
            }
        }

        private void Turn(int direction)
        {
            var scale = Monster.transform.localScale;

            scale.x = Mathf.Sign(direction) * Mathf.Abs(scale.x);

            Monster.transform.localScale = scale;
        }

        private void GetDown()
        {
            Monster.Animator.SetTrigger("GetDown");
            Monster.Controller.center = new Vector3(0, 0.06f);
            Monster.Controller.height = 0.08f;
        }

        private void GetUp()
        {
            Monster.Animator.SetTrigger("GetUp");
            Monster.Controller.center = new Vector3(0, 0.08f);
            Monster.Controller.height = 0.16f;
        }
    }
}