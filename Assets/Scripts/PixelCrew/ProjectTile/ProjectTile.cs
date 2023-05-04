using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.ProjectTile
{
    public class ProjectTile: MonoBehaviour
    {
        [SerializeField] protected Timer.Timer _timeLife;
        [SerializeField] protected float _speed;
        [SerializeField] private UnityEvent _die;
        [SerializeField]private bool isInverting;
        protected Vector2 _position;

        protected virtual void Start()
        {
            _timeLife.Reset();
            _position = transform.position;
            _speed =  isInverting ? -_speed :_speed;

        }

        private void FixedUpdate()
        {
           _position.x += _speed;
            transform.position = new Vector2(_position.x, _position.y);

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