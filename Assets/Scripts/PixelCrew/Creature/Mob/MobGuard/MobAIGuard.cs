using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Creature.MobGuard
{
    public class MobAIGuard : MobAI
    {
        [SerializeField] private UnityEvent _action;
        protected override IEnumerator MovementToHero()
        {
            yield return new WaitForSeconds(_waitForMovement);
            StartCoroutine(AttackFar());
            while (_isVisible.IsTrigger)
            {
                if (_attack.IsTrigger)
                {
                    StartNextCroutine(Attack());
                    StopCoroutine(AttackFar());
                }
                GetDirection();
                yield return null;
            }
            yield return new WaitForSeconds(_waitForPatroling);
            StopCoroutine(AttackFar());
            Patroling();

        }
        
        private IEnumerator AttackFar()
        {
            
            while (_isVisible.IsTrigger &&!_attack.IsTrigger )
            {
                _creature.AttackToCreature(true);
                _action.Invoke();
                yield return new WaitForSeconds(_waitForAttack);
            }
        }

    }
}