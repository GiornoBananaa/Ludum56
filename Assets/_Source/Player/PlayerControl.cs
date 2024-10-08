using System;
using System.Collections;
using System.Collections.Generic;
using EntitySystem.CombatSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerAnimationHandler _animationHandler;
    
    public SpriteRenderer playerSprite_Top;
    public SpriteRenderer playerSprite_Bottom;
    public BoxCollider2D hitBox;
    public BoxCollider2D projectileDestroyerCollider;
    
    private readonly Vector2 _playerCenterOffset = new (0,1);
    private HashSet<IDamageable> _attackAffected = new HashSet<IDamageable>();
    private Vector2 _moveDirection;
    private Camera _camera;
    private float _moveSpeed;
    private bool dealDamage;
    private bool isDead;
    private bool isSkillAvailable = true;
    private bool isAttackAvailable = true;
    private bool isSkill = false;
    private bool isMoving = true;

    private void Awake()
    {
        _camera = Camera.main;
        _moveSpeed = _player.speed;
        projectileDestroyerCollider.enabled = false;
        _player.OnDeath += OnDeath;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
        if (isMoving)
        {
            Move(_moveDirection);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (isAttackAvailable && !isSkill)
        {
            isAttackAvailable = false;
            _animationHandler.PlayAttack();
            DealDamage();
            _attackAffected.Clear();
            Debug.Log("Attack");
            Invoke(nameof(EnableAttack), 1.57f);
        }
    }

    public void OnSkills(InputAction.CallbackContext context)
    {
        if (isSkillAvailable)
        {
            isSkillAvailable = false;
            isSkill = true;
            hitBox.enabled = false;
            
            _moveSpeed = _player.skillSpeed;
            _animationHandler.PlaySkill();
            Invoke(nameof(ShowPlayerSprite), 3f); // ����� �����������
            Invoke(nameof(CancelSkill), 2f); // ����� �����������
            Invoke(nameof(EnableSkills), 6f); // ����� �����������
            Debug.Log("�� ���� DOWN, ��?");
        }
    }
    
    private void Update()
    {
        if(isDead) return;
        if (isMoving)
        {
            Move(_moveDirection);
        }

        if (dealDamage)
        {
            DealDamage();
        }
    }
    
    public void EnableDamageDealer()
    {
        dealDamage = true;
    }
    
    public void DisableDamageDealer()
    {
        dealDamage = false;
    }
    
    private void DealDamage()
    {
        Vector2 mousePosition = _camera.ScreenToWorldPoint(Mouse.current.position.value);
        Vector2 direction = (mousePosition - (Vector2)transform.position + _playerCenterOffset).normalized;
        Vector2 center = transform.position + (Vector3)_playerCenterOffset;
        projectileDestroyerCollider.enabled = true;
        projectileDestroyerCollider.transform.position = center;
        projectileDestroyerCollider.size = new Vector2(_player.attackRadius, _player.attackRadius);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, _player.attackRadius, _player.enemyLayer);
        foreach (var collider in hitColliders)
        {
            Vector2 directionToCollider = collider.transform.position - transform.position + (Vector3)_playerCenterOffset;
            if(Vector2.Angle(directionToCollider, direction) < _player.attackAngle)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                {
                    if(!_attackAffected.Contains(damageable))
                    {
                        damageable.TakeDamage(_player.attackPower);
                        _attackAffected.Add(damageable);
                    }
                }
            }
        }
        Invoke(nameof(DisableProjectileCollider), 0.1f);
    }
    
    private void DisableProjectileCollider()
    {
        projectileDestroyerCollider.enabled = false;
    }
    
    private void ShowPlayerSprite()
    {
        _moveSpeed = _player.speed;
        hitBox.enabled = true; ;
        playerSprite_Top.enabled = true;
        playerSprite_Bottom.enabled = true;
        isSkill = false;
    }
    
    private void CancelSkill()
    {
        _animationHandler.CancelSkill();
    }
    
    private void EnableAttack()
    {
        isAttackAvailable = true;
    }

    private void EnableSkills()
    {
        isSkillAvailable = true;
    }

    private void EnableMovement()
    {
        isMoving = true;
    }

    private void ResumeMoving()
    {
        isMoving = true;
    }

    private void OnDeath()
    {
        isDead = true;
        _animationHandler.PlayDeath();
    }

    private void Move(Vector3 direction)
    {
        if(isDead) return;
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;

        Vector3 moveDirection = new Vector3(direction.x, direction.y);
        transform.position += moveDirection * scaledMoveSpeed;
        _animationHandler.SetMove(moveDirection.magnitude != 0);
        if(moveDirection.x != 0)
            _animationHandler.TurnSprite(isSkill ? moveDirection.x < 0 : moveDirection.x > 0);
    }
}