using System;
using PixelCrew.ColliderBased;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.Creature.LadderMovement
{
    public class HeroMoveOnLadder: MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private StayInLayer _ladderCollider;
        private Rigidbody2D _rigidbody;
        private float _direction;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            ChangeSpeedHero();
        }

        private void ChangeSpeedHero()
        {
            if (_ladderCollider.IsTrigger)
            {
                if (_direction > 0)
                    _rigidbody.velocity = new Vector2(0, _speed);
                else if (_direction < 0)
                    _rigidbody.velocity = new Vector2(0, -_speed);
                else
                    _rigidbody.velocity = new Vector2(0, 0);
            }
            else
            {
                _rigidbody.velocity = new Vector2(0, 0);
      
            }

        }
        
        public void SetDirection(InputAction.CallbackContext call) 
        {
            _direction = call.ReadValue<float>();
        }
    }
}