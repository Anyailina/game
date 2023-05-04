using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Creature
{
    public class MobAIBoss: MonoBehaviour
    {
        [SerializeField] private float _timerForAttack;
        [SerializeField] private UnityEvent _attackAction;

        private void Start()
        {
            StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            while (enabled)
            {
                _attackAction.Invoke();
                yield return new WaitForSeconds(_timerForAttack);
            }
        }
    }
}