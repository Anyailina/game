using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace PixelCrew.Creature.MobGuard
{
    public class MobAIGuard : MobAI
    {
       
      
        protected override IEnumerator MovementToHero()
        {
            yield return new WaitForSeconds(_waitForMovement);
            StartCoroutine(AttackFar());
            while (_isVisible.IsTrigger)
            {
                if (_canAttackCollider.IsTrigger)
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
            while (_isVisible.IsTrigger && _canAttackCollider.IsTrigger)
            {
                _creature.AttackToCreature(true);
                _spawnAction.Invoke();
                yield return new WaitForSeconds(_waitForAttack);
            }
        }
    }
}