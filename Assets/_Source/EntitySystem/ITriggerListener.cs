using UnityEngine;

namespace EntitySystem
{
    public interface ITriggerListener
    {
        void TriggerEnter(Collider2D other);
    }
}