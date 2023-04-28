using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Health
{
    public class HealthComponent: MonoBehaviour
    {
        [SerializeField] private int _hp;
        [SerializeField] private UnityEvent _onDamage;
        [SerializeField] private UnityEvent _OnDie;
        public void changeHp(int quantity)
        {
            Debug.Log(quantity);
            _hp += quantity;
           
             if(  quantity < 0)
            {
                
                _onDamage.Invoke();
            }
            if (_hp <= 0)
            {
                 _OnDie.Invoke();
             }
        }
    }
}