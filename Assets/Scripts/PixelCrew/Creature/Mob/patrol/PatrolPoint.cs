using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Creature.patrol
{
    public class PatrolPoint : Patrol
    {
        [SerializeField] private Transform[] _pointMovementEnemy;
        private Creature _enemy;
        private int currentPoint;

        private void Awake()
        {

            _enemy = GetComponent<Creature>();
        }

        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (isOnPoint())
                {
                    currentPoint = (int)Mathf.Repeat(currentPoint + 1, _pointMovementEnemy.Length);
                }

                var direction = _pointMovementEnemy[currentPoint].position - _enemy.transform.position;
                direction.y = 0;
                _enemy.SetDirection(direction.normalized);
                yield return null;
            }
        }



        private bool isOnPoint()
        {
            return (_pointMovementEnemy[currentPoint].position - _enemy.transform.position).magnitude < 1;
        }
    }



}