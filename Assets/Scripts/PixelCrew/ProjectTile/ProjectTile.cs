using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.ProjectTile
{
    public class ProjectTile: MonoBehaviour
    {
        [SerializeField] private Timer.Timer _timeLife;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private UnityEvent _die;
      
        private bool isInverting;

        private void Start()
        {
            _timeLife.Reset();
            _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
        }

        private void Update()
        {
            if (_timeLife.checkTimer)
            {
                enabled = false;
                _die.Invoke();
            }
        }
        
    }
}