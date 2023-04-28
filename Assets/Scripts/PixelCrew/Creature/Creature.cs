

using System;
using PixelCrew.ColliderBased;
using PixelCrew.Health;
using UnityEngine;

namespace PixelCrew.Creature
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] private CheckOverlap _checkCanAttack;
        [SerializeField] protected Timer.Timer _timerForDamageLazer;
        [SerializeField]  public  float speed = 1;
        public Vector2 _direction;
        protected Rigidbody2D _rigidbody;
        protected Animator _animatorCreature;
        private static readonly int IsRunning = Animator.StringToHash("isRunning");
      
        private static readonly int velocityX = Animator.StringToHash("velocityX");
        private static readonly int attackCreature = Animator.StringToHash("isAttack");
        private static readonly int attackFar = Animator.StringToHash("isAttackFarFromCreature");
        public bool _isSprinting;
        private float _scaleCreature;
        
       
        protected virtual void Awake()
        {
            
            _rigidbody = GetComponent<Rigidbody2D>();
            _animatorCreature = GetComponent<Animator>();
        }

        private void Start()
        {
            _scaleCreature = GetComponent<Transform>().localScale.x;
        }

        public void SetDirection(Vector2 location)
        {
            _direction = location;
        }

        protected  virtual void FixedUpdate()
        {
            changeVelocity();
            SetAnimation();
            InvertScale();

        }

        protected virtual void changeVelocity()
        {
            _rigidbody.velocity = new Vector2(CalculateMovementHeroX(), _rigidbody.velocity.y);
        }
        
        public void changeVelocityLazer()
        {
           
            _timerForDamageLazer.Reset();
            _rigidbody.velocity = new Vector2(-_rigidbody.velocity.x, _rigidbody.velocity.y);
          


        }

        public virtual void takeDamageLazer(GameObject go)
        {

            var damage = go.GetComponent<ModifyHealthComponent>();
            if (damage != null)
            {

                damage.applyDamage(gameObject);
            }


        }


        private void InvertScale()
        {
            if (_direction.x > 0 )
            {
                transform.localScale = new Vector3(_scaleCreature,_scaleCreature,1);
            }
            else if (_direction.x < 0)
            {
                transform.localScale = new Vector3(-_scaleCreature,_scaleCreature,1);
            }
        }

        protected virtual float CalculateMovementHeroX()
        {
            return _direction.x * speed;
        }

        public void attackToCreature(bool isFar)
        {
            if (isFar)
            {
                _animatorCreature.SetTrigger(attackFar);
            }
            else
            {
                _animatorCreature.SetTrigger(attackCreature);
            }
            
        }
       
        

        public void DoAttackNextToCreature(){
            
            _checkCanAttack.check();
        }

        protected virtual void SetAnimation()
        {
            _animatorCreature.SetBool(IsRunning,_direction.x != 0);
           
            _animatorCreature.SetInteger(velocityX, Mathf.RoundToInt(_rigidbody.velocity.x));
        }

        
    }

}



