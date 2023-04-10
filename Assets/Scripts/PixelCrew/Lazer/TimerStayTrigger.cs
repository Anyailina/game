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
        private bool isStay;
        private float time;


      

       
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag(_tag))
            {
                isStay = true;
                time = Time.time + _timer;
                _action.Invoke(gameObject);
                
            }
        }
        

        private void OnTriggerExit2D(Collider2D other)
        {
            isStay = false;
           
        }

        private void Update()
        {
            
            if (isStay)
            {
                if (time < Time.time)
                {
                    Debug.Log("nsjdk");
                    time += _timer;
                    _action.Invoke(gameObject);
                }
            }
        }
        
       
    }
}