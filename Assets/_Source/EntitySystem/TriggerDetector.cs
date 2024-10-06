using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace EntitySystem
{
    public class TriggerDetector : MonoBehaviour
    {
        [Serializable]
        protected struct LayerCollisionEvent
        {
            public LayerMask LayerMask;
            public UnityEvent Event;
        }
        
        [SerializeField] private LayerCollisionEvent[] _eventsOnCollision;
        
        private readonly Dictionary<ITriggerListener, LayerMask> _triggerListener = new();

        public void Subscribe(ITriggerListener listener, LayerMask layerMask)
        {
            _triggerListener.Add(listener, layerMask);
        }

        public void Subscribe(ITriggerListener listener)
        {
            _triggerListener.Add(listener, Physics.AllLayers);
        }

        public void UnSubscribe(ITriggerListener listener)
        {
            _triggerListener.Remove(listener);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            foreach (var listener in _triggerListener)
            {
                if (listener.Value.Contains(other.gameObject.layer))
                {
                    listener.Key.TriggerEnter(other);
                }
            }
            foreach (var eventOnCollision in _eventsOnCollision)
            {
                if(eventOnCollision.LayerMask.Contains(other.gameObject.layer))
                {
                    eventOnCollision.Event?.Invoke();
                }
            }
        }
    }
}