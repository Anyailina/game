using System;
using System.Collections;
using UnityEngine;

namespace PixelCrew.Creature.patrol
{
    public class PatrolPoint : Patrol
    {
        [SerializeField] private Transform[] _pointMovementEnemy;
        private Creature _enemy;
        private int _currentPoint;

        private void Awake()
        {
            _enemy = GetComponent<Creature>();
        }

        public override IEnumerator DoPatrol()
        {
            while (enabled)
            {
                if (isOnPoint())
                    _currentPoint = (int)Mathf.Repeat(_currentPoint + 1, _pointMovementEnemy.Length);
                var direction = _pointMovementEnemy[_currentPoint].position - _enemy.transform.position;
                direction.y = 0;
                _enemy.SetDirection(direction.normalized);
                yield return null;
            }
        }
        
        private bool isOnPoint()
        {
            return (_pointMovementEnemy[_currentPoint].position - _enemy.transform.position).magnitude < 1;
        }
    }



}