using System;
using System.Collections;
using PixelCrew.ColliderBased;
using PixelCrew.Creature.Hero;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace PixelCrew.Laser
{
    public class TimerStayTrigger : MonoBehaviour
    {
        
        [SerializeField]private string _tag;
        [SerializeField] private float _timer = 10;
        [SerializeField] private EnterTrigger.EnterEvent _action;
        private bool _isStay;
        private float _time;
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(_tag))
            {
                _isStay = true;
                _time = Time.time + _timer;
                _action.Invoke(gameObject);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            _isStay = false;
        }
        private void Update()
        {
            if (_isStay)
            {
                if (_time < Time.time)
                {
                    _time += _timer;
                    _action.Invoke(gameObject);
                }
            }
        }
        
       
    }
}