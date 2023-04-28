using System.Collections;
using UnityEngine;

namespace PixelCrew.Creature.patrol
{
    public abstract class  Patrol : MonoBehaviour
    {
        public  abstract  IEnumerator DoPatrol();

    }
}