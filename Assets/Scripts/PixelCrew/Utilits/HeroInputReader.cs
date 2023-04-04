using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.Utilits
{
    
    public class HeroInputReader : MonoBehaviour
    {
        private Creature.Creature _hero;
        private void Awake()
        {
            _hero = GetComponent<Creature.Creature>();
        }

        public void MovementHero(InputAction.CallbackContext call)
        {
            var direction = call.ReadValue<Vector2>();
            _hero?.SetDirection(direction);

        }
        public void Sprint(InputAction.CallbackContext call)
        {
            if (call.performed)
            {
                _hero?.sprintMovement();
            }
        }
        
        
    }
}
