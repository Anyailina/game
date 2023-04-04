
using UnityEngine;
using UnityEngine.Serialization;

namespace PixelCrew.Creature.Hero
{
    public class Hero: Creature
    {
        
        [SerializeField] private float speedJump = 1;
        

        protected override void changeVelocity()
        {
            _rigidbody.velocity = new Vector2(CalculateMovementHeroX(), CalculateMovementHeroY());
        }


        private float CalculateMovementHeroY()
        {
            var velocityJump = _rigidbody.velocity.y;
            var pressingUp = _direction.y > 0;
            if (_isGrouning) _isJumping = false;
            if (pressingUp)
            {
                if (_isGrouning.isTrigger && _rigidbody.velocity.y < 0.01)
                {
                    _isJumping = true;
                    velocityJump = speedJump;
                  
                }
            }
            else if (_isJumping && _rigidbody.velocity.y < 0.01)
            {
                velocityJump *= 0.5f;
            }
            

            return velocityJump;
        }

    }
}