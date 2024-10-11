using System;
using System.Collections;
using System.Collections.Generic;
using EntitySystem;
using EntitySystem.CombatSystem;
using UnityEngine;
using UnityEngine.Pool;

public class OrbPassiveAttack : MonoBehaviour, ITriggerListener
{
    [SerializeField] private TriggerDetector _orbPrefab;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private int _damage;
    [SerializeField] private float _radius;
    [SerializeField] private float _rotationSpeed;

    private readonly List<TriggerDetector> _orbs = new List<TriggerDetector>();
    private ObjectPool<TriggerDetector> _orbPool;
    private float _startAngle;
    
    public Action OnEnemyKilled;

    private void Awake()
    {
        _orbPool = new ObjectPool<TriggerDetector>(CreateOrb);
    }

    private void Update()
    {
        if(_orbs.Count > 0)
            RotateOrbs();
    }

    public void AddOrb()
    {
        TriggerDetector orb = _orbPool.Get();
        orb.Subscribe(this, _enemyLayer);
        orb.gameObject.SetActive(true);
        _orbs.Add(orb);
    }
    
    private TriggerDetector CreateOrb()
    {
        TriggerDetector orb = Instantiate(_orbPrefab);
        return orb;
    }

    private void RotateOrbs()
    {
        _startAngle += Time.deltaTime * _rotationSpeed;
        float step = 360f / _orbs.Count;
        float angle = _startAngle;
        foreach (var orb in _orbs)
        {
            Vector3 offset = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.up * _radius;
            orb.transform.position = transform.position + offset;
            angle += step;
        }
    }
    
    private void DealDamage(GameObject enemy)
    {
        if (enemy.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
            OnEnemyKilled?.Invoke();
        }
    }

    public void TriggerEnter(Collider2D other)
    {
        DealDamage(other.gameObject);
    }
}