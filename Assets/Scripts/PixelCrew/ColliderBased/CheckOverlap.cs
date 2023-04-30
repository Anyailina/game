using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace PixelCrew
{
    public class CheckOverlap: MonoBehaviour
    {
        [SerializeField] private  LayerMask _layer;
        [SerializeField] private string _tag;
        [SerializeField] private float _radius;
        [SerializeField] private OverlapEvent _action;
        
        public void Check()
        {
            var gos = Physics2D.OverlapCircleAll(transform.position, _radius, _layer);
            foreach (var go in gos)
            {
                if (go.gameObject.CompareTag(_tag))
                    _action.Invoke(go.gameObject);
            }
        }

        [Serializable]
        public class OverlapEvent : UnityEvent<GameObject>
        {
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(transform.position,_radius);
        }
    }
    
}