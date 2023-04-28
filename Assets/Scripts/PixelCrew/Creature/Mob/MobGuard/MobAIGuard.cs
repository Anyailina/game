using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Creature.MobGuard
{
    public class MobAIGuard : MobAI
    {
        [SerializeField] private UnityEvent _action;
        protected override IEnumerator movementToHero()
        {
            yield return new WaitForSeconds(waitForMovement);
            StartCoroutine(attack());
            while (_canMovement.isTrigger)
            {
                if (_attack.isTrigger)
                {
                    StopCoroutine(attack());
                    StartNextCroutine(Attack());
                }
                else
                    GetDirection();
                yield return null;
            }
            yield return new WaitForSeconds(waitForPatroling);
            Patroling();

        }

        private IEnumerator attack()
        {
            _creature.attackToCreature(true);
            _action.Invoke();
            yield return new WaitForSeconds(waitForAttack);
        }
    }
}