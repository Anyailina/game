using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.ColliderBased
{
    public class EnterTrigger: MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private EnterEvent _action;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if(!string.IsNullOrEmpty(_tag) && !col.CompareTag(_tag)) return;
            _action?.Invoke(col.gameObject);

        }

        [Serializable]
        public class EnterEvent : UnityEvent<GameObject>
        {
        }
    }
}