using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace PixelCrew.Creature.LadderMovement
{
    public class HeroAttackOnLadder: MonoBehaviour
    {
        [SerializeField] private UnityEvent _spawnAction;
        [SerializeField] private Animator _animatorHero;
        private static readonly int _isArrackAnimation = Animator.StringToHash("IsAttack");
      
        public void AttackHeroOnLadder()
        {
            _spawnAction.Invoke();
        }

        public void AttackUserReader(InputAction.CallbackContext context)
        {
            if (context.performed)
                _animatorHero.SetTrigger(_isArrackAnimation);
        }
    }
}