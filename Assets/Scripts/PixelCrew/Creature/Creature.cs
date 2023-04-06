

using PixelCrew.ColliderBased;
using PixelCrew.Health;
using UnityEngine;

namespace PixelCrew.Creature
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] protected StayInLayer _isGrouning;
        [SerializeField] protected Timer.Timer _timerForDamageLazer;
        [SerializeField] protected float speed = 1;
        protected bool _isSprinting;
        protected Vector2 _direction;
        protected bool _isJumping;
        protected HealthComponent _healthComponent;
        protected Rigidbody2D _rigidbody;
        protected Animator _animatorCreature;
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
        private static readonly int IsGrounding = Animator.StringToHash("isGrounding");
        private static readonly int IsJumping = Animator.StringToHash("isJumping");
        private static readonly int velocityY = Animator.StringToHash("velocityY");
        private static readonly int velocityX = Animator.StringToHash("velocityX");
        private float _scaleCharoctor = 1.6f;
       
        protected virtual void Awake()
        {
            _healthComponent = GetComponent<HealthComponent>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animatorCreature = GetComponent<Animator>();
        }

        public void SetDirection(Vector2 location)
        {
            _direction = location;
        }

        private  void FixedUpdate()
        {
            changeVelocity();
            SetAnimation();
            InvertScale();

        }

        protected virtual void changeVelocity()
        {
            _rigidbody.velocity = new Vector2(CalculateMovementHeroX(), _rigidbody.velocity.y);
        }
        
        public void takeDamageLazer(int damage)
        {
           
            if (!_isSprinting)
            {
                _isJumping = false;
                _rigidbody.velocity = new Vector2(-_rigidbody.velocity.x, _rigidbody.velocity.y);
                _timerForDamageLazer.Reset();
                _healthComponent.changeHp(damage);
            }
           
        }

        private void InvertScale()
        {
            if (_direction.x > 0 )
            {
                transform.localScale = new Vector3(_scaleCharoctor,_scaleCharoctor,1);
            }
            else if (_direction.x < 0)
            {
                transform.localScale = new Vector3(-_scaleCharoctor,_scaleCharoctor,1);
            }
        }

        protected virtual float CalculateMovementHeroX()
        {
            var velocityX = _rigidbody.velocity.x;
            if (_timerForDamageLazer.checkTimer)
            {
                velocityX = _direction.x * speed;
            }
                
            return velocityX;
        }

        private void SetAnimation()
        {
            _animatorCreature.SetBool(IsRunning,_direction.x != 0);
            _animatorCreature.SetBool(IsJumping,_direction.y > 0);
            _animatorCreature.SetBool(IsGrounding,_isGrouning.isTrigger );
            _animatorCreature.SetFloat(velocityY, _rigidbody.velocity.y);
            _animatorCreature.SetInteger(velocityX, Mathf.RoundToInt(_rigidbody.velocity.x));
        }

        
    }

}



