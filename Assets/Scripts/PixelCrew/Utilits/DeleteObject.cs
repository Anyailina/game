using UnityEngine;

namespace PixelCrew.Utilits
{
    public class DeleteObject : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;

        public void DestroyObject()
        {
            Destroy(_gameObject);
        }
    }
}