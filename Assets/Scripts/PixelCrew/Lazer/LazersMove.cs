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
        [SerializeField] private bool isLeftSideHero;
        [SerializeField] private EnterTrigger.EnterEvent _damageHero;
        
        public void checkDirectionHero(GameObject go)
        {
            if (!_hero._isSprinting)
            {
                var directionIsRight = transform.position.x - _hero.transform.position.x > 0;
                if (isLeftSideHero && directionIsRight || !isLeftSideHero && !directionIsRight)
                {
                   
                    _hero.changeVelocityLazer();
                    _damageHero.Invoke(go);
                    
                }
                
            }
        }
        
    }

}