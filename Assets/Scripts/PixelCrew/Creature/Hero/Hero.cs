

using System.Collections;
using PixelCrew.ColliderBased;
using UnityEngine;
using UnityEngine.Serialization;


namespace PixelCrew.Creature.Hero
{
    public class Hero : Creature
    {
        [SerializeField] private float _velocityFallDuringClimbing;
        [SerializeField] private float _velocityClimbingX = 15f;
        [SerializeField] private float _velocityClimbingY = 12f ;

        [SerializeField]private Timer.Timer _timerClimbing;
        [SerializeField] private float _timerForDurationSprint;
        [SerializeField] private float speedJump = 1;
        [SerializeField] private float sprint = 1;
        [FormerlySerializedAs("_isClimbing")] [SerializeField] private StayInLayer _isClimbingCollider;
        private static readonly int isSprintingAnimation = Animator.StringToHash("isSprinting");
        private static readonly int isClimbingAnimation = Animator.StringToHash("isClimbing");
        private float _gravityScale;
        private bool isClimbing;
        private bool isJumping;
    
        private bool pressedUp => _direction.y > 0;
      


        protected override void Awake()
        {
            base.Awake();
            _gravityScale = _rigidbody.gravityScale;

        }
        protected override void changeVelocity()
        {
            var heroMoveToAnotherDirection = _direction.x > 0 && transform.localScale.x < 0 ||
                                             _direction.x < 0 && transform.localScale.x > 0;
            if (isClimbing && pressedUp && heroMoveToAnotherDirection)
            {
                _rigidbody. velocity =new Vector2(_velocityClimbingX*_direction.x,_velocityClimbingY);
                isJumping = true;
            }
            else
                _rigidbody.velocity = new Vector2(CalculateMovementHeroX(), Mathf.Round( 1000f*CalculateMovementHeroY())/ 1000f);

        }

        
 
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            climbHero();
        }

        private void  climbHero()
        {
            var jumpNextToGround = isJumping && _direction.x == 0;
            if (jumpNextToGround) return;
            if (!_isClimbingCollider.isTrigger)
                isClimbing = false;

            hangingHeroDuringClimbing();
            fallMoveHeroDuringClimbing();
        }

        private void hangingHeroDuringClimbing()
        {
            
            if (_isClimbingCollider.isTrigger  && !isClimbing )
            {
               
                isClimbing = true;
                _rigidbody.gravityScale = 0;
                _rigidbody.velocity = new Vector2(0, 0);
                isJumping = false;
                _timerClimbing.Reset();
            }
        }

        private void fallMoveHeroDuringClimbing()
        {
            var isHangingTooLong = _timerClimbing.checkTimer && _isClimbingCollider.isTrigger;
            if  ( isClimbing && ( isHangingTooLong || !pressedUp ))
            {
                _rigidbody.gravityScale = _gravityScale;
                isJumping = false;
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x,-_velocityFallDuringClimbing );
            }
        }

        private float CalculateMovementHeroY()
        {
            if (_isGrouning.isTrigger)
            {
                isJumping = false;
                isClimbing = false;
            }
            if (_isSprinting) return 0f;
            if (!_isSprinting && !isClimbing ) _rigidbody.gravityScale = _gravityScale;
            if (isClimbing) return _rigidbody.velocity.y;
            
            var velocityJump = _rigidbody.velocity.y;
           
            if (pressedUp)
            {
                if (_isGrouning.isTrigger && _rigidbody.velocity.y < 0.01)
                {
                    isJumping = true;
                    velocityJump = speedJump;
                }
            }
            return velocityJump;
        }


        protected override float CalculateMovementHeroX()
        {
            
            var velocityX = _rigidbody.velocity.x;
            if (_timerForDamageLazer.checkTimer)
            {
                velocityX = _direction.x * speed;
            }
            if (_isSprinting )
                velocityX = transform.lossyScale.x > 0 ? sprint : -sprint;
          
            if (isClimbing) return _rigidbody.velocity.x;
            return velocityX;
        }
        

        public void StartSprint()
        {
            if (_direction.y > 0)
            {
                _rigidbody.gravityScale = 0;
            }
            var time = Time.time + _timerForDurationSprint;
            _isSprinting = true;
            _animatorCreature.SetBool(isSprintingAnimation, true);
            StartCoroutine(SprintCoroutine(time));


        }

        private IEnumerator SprintCoroutine(float time)
        {
            while (Time.time < time)
            {
                if (!_isSprinting)
                {
                    yield break;
                }
                yield return null;
            }

            _isSprinting = false;
            _animatorCreature.SetBool(isSprintingAnimation,false);
        }

        protected override void SetAnimation()
        {
            base.SetAnimation();
            _animatorCreature.SetBool(isClimbingAnimation,isClimbing);
            
        }
    }
    




}