using System;
using System.Collections;
using PixelCrew.ColliderBased;
using PixelCrew.Creature.patrol;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Serialization;

namespace PixelCrew.Creature
{
    public class MobAI: MonoBehaviour
    {
        [SerializeField] private StayInLayer _attack;
        [SerializeField] private StayInLayer _canMovement;
        [SerializeField ]private Creature _creature;
        private IEnumerator _currentCoroutine;
        private float waitForAttack = 1f;
        private float waitForPatroling = 1f;
        private float waitForMovement = 0.1f;
        private Hero.Hero _hero;
        private Patrol _patrol;
        
        private void Start()
        {
            _hero = FindObjectOfType<Hero.Hero>();
           

        }

        public void  HeroIsVisible()
        {
            StartNextCroutine(movementToHero());
        }

        private void GetDirection()
        {
            var direction = _hero.transform.position - transform.position ;
            direction.z = transform.position.z;
            _creature.SetDirection(direction.normalized);
        }

        private IEnumerator movementToHero()
        {
            yield return new WaitForSeconds(waitForMovement);
            while (_canMovement.isTrigger)
            {
                if (_attack.isTrigger)
                    StartNextCroutine(Attack());
                else
                    GetDirection();

                yield return null;
            }
            yield return new WaitForSeconds(waitForPatroling);
          

        }

        private  IEnumerator Attack()
        {
           
            while (_attack.isTrigger)
            {
              
                _creature.attack();
                yield return new WaitForSeconds(waitForAttack);

            }
           
            StartNextCroutine(movementToHero());
            
        }


        private void StartNextCroutine(IEnumerator corountine)
        {
            
            _creature.SetDirection(Vector2.zero);
            
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            _currentCoroutine = corountine;
            StartCoroutine(corountine);
        }
    }
}