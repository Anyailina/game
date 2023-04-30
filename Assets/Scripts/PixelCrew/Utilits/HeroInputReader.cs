using System;
using PixelCrew.Creature.Hero;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.Utilits
{
    
    public class HeroInputReader : MonoBehaviour
    {
        private Hero _hero;
        
        private void Awake()
        {
            _hero = GetComponent<Hero>();
        }
        
        public void MovementHero(InputAction.CallbackContext call)
        {
            var direction = call.ReadValue<Vector2>();
            _hero?.SetDirection(direction);
        }
        
        public void Sprint(InputAction.CallbackContext call)
        {
            if (call.started)
                _hero?.StartSprint();
        }
        
        public void AttackReader(InputAction.CallbackContext call)
        {
            if (call.started )
                _hero.CheckTimerForAtttack();
        }

    }
}
