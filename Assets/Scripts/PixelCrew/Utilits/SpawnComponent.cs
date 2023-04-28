using UnityEngine;

namespace PixelCrew.Utilits
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _particle;
        public void Spawn()
        {
           
            var spawner = Instantiate(_particle, transform.position,Quaternion.identity);
            spawner.transform.localScale = transform.lossyScale;
        }
    }
}