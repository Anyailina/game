
using System.Collections;
using PixelCrew.ColliderBased;
using PixelCrew.Creature.patrol;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


namespace PixelCrew.Creature
{
    public class MobAI: MonoBehaviour
    {
        [SerializeField] protected StayInLayer _canAttackCollider; 
        [SerializeField] protected StayInLayer _isVisible;
        [SerializeField ]protected Creature _creature;
        [SerializeField] protected UnityEvent _spawnAction;
        private IEnumerator _currentCoroutine;
        protected float _waitForAttack = 1f;
        protected float _waitForPatroling = 1f;
        protected float _waitForMovement = 0.5f;
        protected Hero.Hero _hero;
        private Patrol _patrol;
        private bool _isDied;
        
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
         
            if (_isDied) return;
            StartNextCroutine(MovementToHero());
        }

        protected virtual void GetDirection()
        {
            var direction = _hero.transform.position - transform.position ;
            direction.z = transform.position.z;
            _creature.SetDirection(direction.normalized);
        }

        protected virtual IEnumerator MovementToHero()
        {
            yield return new WaitForSeconds(_waitForMovement);
            while (_isVisible.IsTrigger)
            {
                if (_canAttackCollider.IsTrigger)
                    StartNextCroutine(Attack());
                else
                    GetDirection();
                
                yield return null;
            }
            yield return new WaitForSeconds(_waitForPatroling);
            Patroling();
        }

        public  virtual void  IsDying()
        {
            StopAllCoroutines(); ;
            _creature.SetDirection(Vector2.zero);
            _isDied = true;
        }

        protected   IEnumerator Attack()
        {
            while (_canAttackCollider.IsTrigger)
            {
                _creature.AttackToCreature(false);
                yield return new WaitForSeconds(_waitForAttack);
            }
            StartNextCroutine(MovementToHero());
            
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