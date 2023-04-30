using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace PixelCrew.Health
{
    public class HealthComponent: MonoBehaviour
    {
        [SerializeField] private int _hp;
        [SerializeField] private UnityEvent _onDamage; 
        [SerializeField] private UnityEvent _onDie;
        public void ChangeHp(int quantity)
        {
            _hp += quantity;
            if (quantity < 0)
                _onDamage.Invoke();
            if (_hp <= 0)
                _onDie.Invoke();
        }
    }
}