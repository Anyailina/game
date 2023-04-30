using UnityEngine;

namespace PixelCrew.Health
{
    public class ModifyHealthComponent: MonoBehaviour
    {
        [SerializeField] private int _hp;

        public void ApplyDamage(GameObject go)
        {
            var health = go.GetComponent<HealthComponent>();
            if (health != null)
            {
                health.ChangeHp(_hp);
            }
        }
    }
}