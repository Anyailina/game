

using System;
using PixelCrew.ColliderBased;
using PixelCrew.Health;
using UnityEngine;
using UnityEngine.Serialization;

namespace PixelCrew.Creature
{
    public class Creature : MonoBehaviour
    {
        [SerializeField] private CheckOverlap _checkCanAttack;
        [SerializeField] protected Timer.Timer _timerForDamageLazer;
        [SerializeField]  public  float Speed = 1;
        protected Vector2 _direction;
        protected Rigidbody2D _rigidbody;
        protected Animator _animatorCreature;
        private static readonly int _isRunning = Animator.StringToHash("isRunning");
        private static readonly int _velocityX = Animator.StringToHash("velocityX");
        private static readonly int _attackCreature = Animator.StringToHash("isAttack");
        private static readonly int _attackFar = Animator.StringToHash("isAttackFarFromCreature");
        public bool IsSprinting;
        private float _scaleCreature;
        private bool _isDying;
        
       
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
            ChangeVelocity();
            SetAnimation();
            InvertScale();
        }

        protected virtual void ChangeVelocity()
        {
            _rigidbody.velocity = new Vector2(CalculateMovementHeroX(), _rigidbody.velocity.y);
        }
        
        public void ChangeVelocityLazer()
        {
            _timerForDamageLazer.Reset();
            _rigidbody.velocity = new Vector2(-_rigidbody.velocity.x, _rigidbody.velocity.y);
        }

        public virtual void takeDamageLazer(GameObject go)
        {
            var damage = go.GetComponent<ModifyHealthComponent>();
            if (damage != null)
                damage.ApplyDamage(gameObject);
        }


        private void InvertScale()
        {
            if (_direction.x > 0 )
                transform.localScale = new Vector3(_scaleCreature,_scaleCreature,1);
            
            else if (_direction.x < 0)
                transform.localScale = new Vector3(-_scaleCreature,_scaleCreature,1);
        }

        protected virtual float CalculateMovementHeroX()
        {
            return _direction.x * Speed;
        }

        public void AttackToCreature(bool isFar)
        {
            if (isFar)
                _animatorCreature.SetTrigger(_attackFar);
            else
                _animatorCreature.SetTrigger(_attackCreature);
        }
       
        

        public void DoAttackNextToCreature()
        {
            _checkCanAttack.Check();
        }

        protected virtual void SetAnimation()
        {
            _animatorCreature.SetBool(_isRunning,_direction.x != 0);
            _animatorCreature.SetInteger(_velocityX, Mathf.RoundToInt(_rigidbody.velocity.x));
        }

        
    }

}



