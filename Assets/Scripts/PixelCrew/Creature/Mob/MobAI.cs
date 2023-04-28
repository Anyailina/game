
using System.Collections;
using PixelCrew.ColliderBased;
using PixelCrew.Creature.patrol;

using UnityEngine;


namespace PixelCrew.Creature
{
    public class MobAI: MonoBehaviour
    {
        [SerializeField] protected StayInLayer _attack;
        [SerializeField] protected StayInLayer _canMovement;
        [SerializeField ]protected Creature _creature;
        private IEnumerator _currentCoroutine;
        protected float waitForAttack = 1f;
        protected float waitForPatroling = 1f;
        protected float waitForMovement = 0.1f;
        private Hero.Hero _hero;
        private Patrol _patrol;
        private bool isDied;
        
        private void Awake()
        {
            _hero = FindObjectOfType<Hero.Hero>();
            _patrol = GetComponent<Patrol>();


        }

        private void Start()
        {
            Patroling();
        }

        protected void Patroling()
        {
            
           StartNextCroutine( _patrol.DoPatrol());
        }

        public void  HeroIsVisible()
        {
            if (isDied) return;
            StartNextCroutine(movementToHero());
        }

        protected void GetDirection()
        {
            var direction = _hero.transform.position - transform.position ;
            direction.z = transform.position.z;
            _creature.SetDirection(direction.normalized);
        }

        protected virtual IEnumerator movementToHero()
        {
            yield return new WaitForSeconds(waitForMovement);
            while (_canMovement.isTrigger)
            {
                if (_attack.isTrigger)
                    StartNextCroutine(Attack());
                else
                {
                    GetDirection();
                }
                  

                yield return null;
            }
            yield return new WaitForSeconds(waitForPatroling);
            Patroling();


        }

        public void  isDying()
        {
            StopAllCoroutines();
            _creature.SetDirection(Vector2.zero);
            isDied = true;
        }

        protected   IEnumerator Attack()
        {
           
            while (_attack.isTrigger)
            {
              
                _creature.attackToCreature(false);
                yield return new WaitForSeconds(waitForAttack);

            }
           
            StartNextCroutine(movementToHero());
            
        }


        protected void StartNextCroutine(IEnumerator corountine)
        {
            
            _creature.SetDirection(Vector2.zero);
            
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            _currentCoroutine = corountine;
            StartCoroutine(corountine);
        }
    }
}