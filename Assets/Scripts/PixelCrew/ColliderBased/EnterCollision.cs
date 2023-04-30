using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.ColliderBased
{
    public class EnterCollision: MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private EnterTrigger.EnterEvent _action;
        private void OnCollisionEnter2D(Collision2D col)
        {
            if(!string.IsNullOrEmpty(_tag) && !col.gameObject.CompareTag(_tag)) return;
            
            _action?.Invoke(col.gameObject);

        }
    }
}