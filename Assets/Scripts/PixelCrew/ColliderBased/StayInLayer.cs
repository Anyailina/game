using System;
using UnityEngine;

namespace PixelCrew.ColliderBased
{
    public class StayInLayer: MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private Collider2D _collider;
        private  bool _isTrigger;
        public bool isTrigger => _isTrigger;
        

        private void OnTriggerStay2D(Collider2D other)
        {
            _isTrigger = _collider.IsTouchingLayers(_layer); 
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isTrigger = _collider.IsTouchingLayers(_layer); 
        }

        private void OnDrawGizmos()
        {

            Debug.DrawLine(transform.position, Vector3.up, isTrigger ? Color.green : Color.red);
        }
    }
}