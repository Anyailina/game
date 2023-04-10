

using System.Collections;
using PixelCrew.ColliderBased;
using UnityEngine;
using UnityEngine.Serialization;


namespace PixelCrew.Creature.Hero
{
    public class Hero : Creature
    {
        
        [SerializeField] private float _timerForDurationSprint;
        [SerializeField] private float speedJump = 1;
        [SerializeField] private float sprint = 1;
        [SerializeField] private StayInLayer _isClimbing;
        private static readonly int isSprinting = Animator.StringToHash("isSprinting");
        private float _gravityScale;
        private bool isClimbing;


        protected override void Awake()
        {
            base.Awake();
            _gravityScale = _rigidbody.gravityScale;

        }
        protected override void changeVelocity()
        {
            _rigidbody.velocity = new Vector2(CalculateMovementHeroX(), Mathf.Round( 1000f*CalculateMovementHeroY())/ 1000f);
            
        }

        
 
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (_isClimbing.isTrigger)
            {
                climbHero();
            }
        }

        private void  climbHero()
        {
            if (_rigidbody.velocity.y > 1 && !_isGrouning.isTrigger)
            {
                isClimbing = true;
                _rigidbody.gravityScale = 0;
            }
            else
            {
                isClimbing = false;
                _rigidbody.gravityScale = _gravityScale;
            }
            
        }


        private float CalculateMovementHeroY()
        {
            if (_isSprinting) return 0f;
 
            var velocityJump = _rigidbody.velocity.y;
            if (!_isSprinting) _rigidbody.gravityScale = _gravityScale;
            var pressingUp = _direction.y > 0;
            if (_isGrouning) 
                _isJumping = false;
            if (pressingUp && !isClimbing)
            {
                if (_isGrouning.isTrigger && _rigidbody.velocity.y < 0.01)
                {
                    _isJumping = true;
                    velocityJump = speedJump;
                }
            }
            else if (_isJumping && _rigidbody.velocity.y < 0.01)
                velocityJump *= 0.5f;
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
            return velocityX;
        }
        

        public void StartSprint()
        {
            if (_direction.x == 0 && _direction.y == 0) return;

            if (_direction.y > 0)
            {
                _rigidbody.gravityScale = 0;
            }
            var time = Time.time + _timerForDurationSprint;
            _isSprinting = true;
            _animatorCreature.SetBool(isSprinting, true);
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
            _animatorCreature.SetBool(isSprinting,false);
        }
        
        
    }




}