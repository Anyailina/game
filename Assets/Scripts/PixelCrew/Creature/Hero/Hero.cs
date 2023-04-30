

using System.Collections;
using PixelCrew.ColliderBased;
using UnityEngine;
using UnityEngine.Serialization;


namespace PixelCrew.Creature.Hero
{
    public class Hero : Creature
    {
        [SerializeField] private StayInLayer _isGrouning;
        [SerializeField] private float _velocityClimbingX = 15f;
        [SerializeField] private float _velocityClimbingY = 12f ;
        [SerializeField]private Timer.Timer _timerClimbing;
        [SerializeField]private Timer.Timer _timerforAttack;
        [SerializeField] private float _timerForDurationSprint;
        [SerializeField] private float _speedJump = 1;
        [SerializeField] private float _sprint = 1;
        private static readonly int _isGrounding = Animator.StringToHash("isGrounding");
        private static readonly int _velocityY = Animator.StringToHash("velocityY");
        [SerializeField] private StayInLayer _isClimbingCollider;
        private static readonly int _isSprintingAnimation = Animator.StringToHash("isSprinting");
        private static readonly int _isClimbingAnimation = Animator.StringToHash("isClimbing");
        private float _gravityScale;
        private bool _isOnWall;
        private bool _isHang;
        private bool _isJumping;
        private bool _isChangingWall;
        private bool timerForClimbingEnded; 
        private bool _pressedUp => _direction.y > 0;
        
        protected override void Awake()
        {
            base.Awake();
            _gravityScale = _rigidbody.gravityScale;

        }
        protected override void ChangeVelocity()
        {
            if (!_isChangingWall && !_isOnWall)
                _rigidbody.velocity= new Vector2(CalculateMovementHeroX(), Mathf.Round( 1000f*CalculateMovementHeroY())/ 1000f);
            if (_isGrouning.IsTrigger)
            {
                _isChangingWall = false;
                _isHang = false;
            }
            CheckClimbing();
            ChangeWallDuringClimbing();
            
            CheckMoveOnTheWall();
            FallHeroDuringClimbing();
        }
        
        private void CheckClimbing()
        {
            if ( _pressedUp && _isClimbingCollider.IsTrigger && !_isGrouning.IsTrigger )
                _isHang = true;
            else
                _isHang = false;
        }
        
        private void  ChangeWallDuringClimbing()
        {
            var heroMoveToAnotherDirection = _direction.x > 0 && transform.localScale.x < 0 ||
                                             _direction.x < 0 && transform.localScale.x > 0;
            if ( _isHang && heroMoveToAnotherDirection   )
            {
                _isChangingWall = true;
                _rigidbody.velocity =  new Vector2(_velocityClimbingX*_direction.x,_velocityClimbingY);
            }
            else
                _isChangingWall = false;
        }

        private void CheckMoveOnTheWall()
        {
            var direction = _direction.x > 0 && transform.localScale.x > 0 ||
                            _direction.x < 0 && transform.localScale.x < 0;
            if (_isHang && direction)
            {
                _isOnWall = true;
                _rigidbody.gravityScale = 0;
                _timerClimbing.Reset();
                _rigidbody.velocity = new Vector2(0, 0);
            }
            else
                _isOnWall = false;

        }

        private void  FallHeroDuringClimbing()
        {
            var direction = (_direction.x > 0 && transform.localScale.x > 0 ||
                            _direction.x < 0 && transform.localScale.x < 0) && _pressedUp;
            if ((_isOnWall || _isChangingWall) && (!_timerClimbing.checkTimer && !direction   || direction && _timerClimbing.checkTimer) )
                _rigidbody.gravityScale = _gravityScale;
        }

        public override void takeDamageLazer(GameObject go)
        {
            if (!IsSprinting)
                base.takeDamageLazer(go);
        }

        private float CalculateMovementHeroY()
        {
            if (IsSprinting) return 0f;
            if (!IsSprinting ) _rigidbody.gravityScale = _gravityScale;

            if (_isChangingWall)
                 return _velocityClimbingY;

            var velocityJump = _rigidbody.velocity.y;
           
            if (_pressedUp)
            {
                if (_isGrouning.IsTrigger && _rigidbody.velocity.y < 0.01)
                    velocityJump = _speedJump;
            }
            return velocityJump;
        }


        protected override float CalculateMovementHeroX()
        {
            var velocityX = _rigidbody.velocity.x;
            if (_timerForDamageLazer.checkTimer)
            {
                velocityX = _direction.x * Speed;
            }
            if (IsSprinting )
                velocityX = transform.lossyScale.x > 0 ? _sprint : -_sprint;

            if (_isChangingWall)
            {
                if (_direction.x > 0 && transform.localScale.x > 0)
                    return _velocityClimbingX;
                else
                    return -_velocityClimbingX;

            }
            
            return velocityX;
        }
        

        public void StartSprint()
        {
            if (_direction.y > 0)
            {
                _rigidbody.gravityScale = 0;
            }
            var time = Time.time + _timerForDurationSprint;
            IsSprinting = true;
            _animatorCreature.SetBool(_isSprintingAnimation, true);
            StartCoroutine(SprintCoroutine(time));
            
        }

        private IEnumerator SprintCoroutine(float time)
        {
            while (Time.time < time)
            {
                if (!IsSprinting)
                {
                    yield break;
                }
                yield return null;
            }
            IsSprinting = false;
            _animatorCreature.SetBool(_isSprintingAnimation,false);
        }

        public void CheckTimerForAtttack()
        {
            if (_timerforAttack.checkTimer)
            {
                _timerforAttack.Reset();
                AttackToCreature(false);
            }
        }

        protected override void SetAnimation()
        {
            base.SetAnimation();
            _animatorCreature.SetBool(_isGrounding,_isGrouning.IsTrigger );
            _animatorCreature.SetBool(_isClimbingAnimation,_isOnWall);
            _animatorCreature.SetFloat(_velocityY, _rigidbody.velocity.y);
            
        }
    }
    




}