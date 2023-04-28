

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
        [SerializeField] private Timer.Timer _timerForChangeWall;
        [SerializeField]private Timer.Timer _timerforAttack;
        [SerializeField] private float _timerForDurationSprint;
        [SerializeField] private float speedJump = 1;
        [SerializeField] private float sprint = 1;
        private static readonly int IsGrounding = Animator.StringToHash("isGrounding");
        private static readonly int velocityY = Animator.StringToHash("velocityY");
        [SerializeField] private StayInLayer _isClimbingCollider;
        private static readonly int isSprintingAnimation = Animator.StringToHash("isSprinting");
        private static readonly int isClimbingAnimation = Animator.StringToHash("isClimbing");
        private float _gravityScale;
        private bool isOnWall;
        private bool isHang;
        private bool isJumping;
        private bool isChangingWall;

        private bool timerForClimbingEnded;
   
        
       

        private bool pressedUp => _direction.y > 0;


        protected override void Awake()
        {
            base.Awake();
            _gravityScale = _rigidbody.gravityScale;

        }
        protected override void changeVelocity()
        {
            if (!isChangingWall && !isOnWall)
                _rigidbody.velocity= new Vector2(CalculateMovementHeroX(), Mathf.Round( 1000f*CalculateMovementHeroY())/ 1000f);
            if (_isGrouning.isTrigger)
            {
                isChangingWall = false;
                isHang = false;
            }
            checkClimbing();
            ChangeWallDuringClimbing();
            
            checkMoveOnTheWall();
            fallHeroDuringClimbing();
           
            

        }
        
        private void checkClimbing()
        {
            
            if ( pressedUp && _isClimbingCollider.isTrigger && !_isGrouning.isTrigger )
                isHang = true;
            else
                isHang = false;
        }



        private void  ChangeWallDuringClimbing()
        {
           
            var heroMoveToAnotherDirection = _direction.x > 0 && transform.localScale.x < 0 ||
                                             _direction.x < 0 && transform.localScale.x > 0;
           
            if ( isHang && heroMoveToAnotherDirection   )
            {
                isChangingWall = true;
                _rigidbody.velocity =  new Vector2(_velocityClimbingX*_direction.x,_velocityClimbingY);
            }
            else
                isChangingWall = false;
            
               


        }

        private void checkMoveOnTheWall()
        {
            var direction = _direction.x > 0 && transform.localScale.x > 0 ||
                            _direction.x < 0 && transform.localScale.x < 0;
            if (isHang && direction)
            {
                isOnWall = true;
                _rigidbody.gravityScale = 0;
                _timerClimbing.Reset();
                _rigidbody.velocity = new Vector2(0, 0);
            }
            else
                isOnWall = false;

        }

        private void  fallHeroDuringClimbing()
        {
            var direction = (_direction.x > 0 && transform.localScale.x > 0 ||
                            _direction.x < 0 && transform.localScale.x < 0) && pressedUp;
            if ((isOnWall || isChangingWall) && (!_timerClimbing.checkTimer && !direction   || direction && _timerClimbing.checkTimer) )
                _rigidbody.gravityScale = _gravityScale;
        }

        public override void takeDamageLazer(GameObject go)
        {
            if (!_isSprinting)
                base.takeDamageLazer(go);
        }

        private float CalculateMovementHeroY()
        {
            
            
            if (_isSprinting) return 0f;
            if (!_isSprinting ) _rigidbody.gravityScale = _gravityScale;

            if (isChangingWall)
                 return _velocityClimbingY;

            var velocityJump = _rigidbody.velocity.y;
           
            if (pressedUp)
            {
                if (_isGrouning.isTrigger && _rigidbody.velocity.y < 0.01)
                {
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

            if (isChangingWall)
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

        public void checkTimerForAtttack()
        {
            if (_timerforAttack.checkTimer)
            {
                _timerforAttack.Reset();
                attackToCreature(false);
            }
        }

        protected override void SetAnimation()
        {
            base.SetAnimation();
            _animatorCreature.SetBool(IsGrounding,_isGrouning.isTrigger );
            _animatorCreature.SetBool(isClimbingAnimation,isOnWall);
            _animatorCreature.SetFloat(velocityY, _rigidbody.velocity.y);
            
        }
    }
    




}