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

        private void Start()
        {
            _timeLife.Reset();
            
        }

        private void Update()
        {
            _rigidbody.MovePosition(new Vector2(_speed * transform.localScale.x,transform.position.y));
            
            if (_timeLife.checkTimer)
            {
                _die.Invoke();
            }
        }
        
    }
}