using PixelCrew.ColliderBased;
using PixelCrew.Creature.Hero;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace PixelCrew.Laser
{
    public class LazersMove : MonoBehaviour
    {
        [SerializeField]private Creature.Creature _hero;
        [SerializeField] private bool _isLeftSideHero;
        [SerializeField] private EnterTrigger.EnterEvent _damageHero;
        
        public void CheckDirectionHero(GameObject go)
        {
            if (!_hero.IsSprinting)
            {
                var directionIsRight = transform.position.x - _hero.transform.position.x > 0;
                if (_isLeftSideHero && directionIsRight || !_isLeftSideHero && !directionIsRight)
                {
                    _hero.ChangeVelocityLazer();
                    _damageHero.Invoke(go);
                }
                
            }
        }
        
    }

}