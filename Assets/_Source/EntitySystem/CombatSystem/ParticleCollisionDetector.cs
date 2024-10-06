using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace EntitySystem.CombatSystem
{
    public class ParticleCollisionDetector : MonoBehaviour
    {
        [Serializable]
        protected struct LayerCollisionEvent
        {
            public LayerMask LayerMask;
            public UnityEvent Event;
        }
        
        [SerializeField] private LayerCollisionEvent[] _eventsOnCollision;
        
        private readonly Dictionary<IParticleCollisionListener, LayerMask> _listeners = new();

        public void Subscribe(IParticleCollisionListener listener, LayerMask layerMask)
        {
            _listeners.Add(listener, layerMask);
        }
    
        public void Unsubscribe(IParticleCollisionListener listener)
        {
            _listeners.Remove(listener);
        }
    
        private void OnParticleCollision(GameObject other)
        {
            foreach (var listener in _listeners
                         .Where(listener => listener.Value == (listener.Value | (1 << other.layer))))
            {
                listener.Key.OnParticleCollisionEnter(other);
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