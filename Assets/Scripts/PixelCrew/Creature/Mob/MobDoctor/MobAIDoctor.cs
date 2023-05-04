using System.Collections;
using System.Collections.Generic;
using PixelCrew.ColliderBased;
using PixelCrew.Creature.MobGuard;
using UnityEngine;

namespace PixelCrew.Creature.MobDoctor
{
    public class MobAIDoctor: MobAI
    {
        [SerializeField] private StayInLayer _checkGroundHero;
        
        protected override void GetDirection()
        {
            var direction = transform.position - _hero.transform.position ;
            direction.z = transform.position.z;
            _creature.SetDirection(direction.normalized);
        }
        protected override IEnumerator MovementToHero()
        {
            StartCoroutine(AttackFar());
            yield return new WaitForSeconds(_waitForMovement);
            while (_isVisible.IsTrigger)
            {
                if (!_checkGroundHero.IsTrigger)
                    GetDirection();
                else
                    _creature.SetDirection(Vector2.zero);
                yield return null;
            }
            yield return new WaitForSeconds(_waitForPatroling);
            StopCoroutine(AttackFar());
            Patroling();
        }
        
        private IEnumerator AttackFar()
        {
            while (_isVisible.IsTrigger)
            {
                if (_creature.transform.localScale.x * _hero.transform.localScale.x > 0 && !_checkGroundHero.IsTrigger)
                {
                    _creature.AttackToCreature(true);
                    _spawnAction.Invoke();
                    yield return new WaitForSeconds(_waitForAttack);
                }
                else
                    yield return null;
                
            }
        }
        
    }
}