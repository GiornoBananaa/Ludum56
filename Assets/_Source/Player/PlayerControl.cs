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
    [SerializeField] private PlayerAudioPlayer _audioPlayer;
    
    public SpriteRenderer playerSprite_Top;
    public SpriteRenderer playerSprite_Bottom;
    public BoxCollider2D hitBox;
    public BoxCollider2D projectileDestroyerCollider;
    
    private readonly Vector2 _playerCenterOffset = new (0,0);
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
        _player.OnLevelSwitch += OnLevelSwitch;
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
        if(isDead) return;
        Vector2 screenCenter = new Vector2((float)Screen.width/2, (float)Screen.height/2);
        Vector2 direction = (Mouse.current.position.ReadValue() - screenCenter).normalized;
        Vector2 attackCenter = (Vector2)transform.position + direction * _player.attackRadius/2;
        
        projectileDestroyerCollider.enabled = true;
        projectileDestroyerCollider.transform.position = attackCenter;
        projectileDestroyerCollider.size = new Vector2(_player.attackRadius, _player.attackRadius);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _player.attackRadius, _player.enemyLayer);
        foreach (var collider in hitColliders)
        {
            Vector2 directionToCollider = collider.transform.position - transform.position + (Vector3)_playerCenterOffset;
            if(Vector2.Angle(directionToCollider, direction) < _player.attackAngle 
               && Vector2.Distance(transform.position, collider.transform.position) <= _player.attackRadius)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                {
                    if(!_attackAffected.Contains(damageable))
                    {
                        _player.killedEnemies++;
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
        hitBox.enabled = false;
        _animationHandler.PlayDeath();
        _audioPlayer.PlayDeath();
    }
    
    private void OnLevelSwitch()
    {
        isDead = true;
        hitBox.enabled = false;
        _animationHandler.PlayLevelSwitch();
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