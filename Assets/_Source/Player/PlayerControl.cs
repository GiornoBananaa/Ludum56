using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EntitySystem;
using EntitySystem.CombatSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerAnimationHandler _animationHandler;
    [SerializeField] private ParticleSystem _attackParticle;
    [SerializeField] private PlayerAudioPlayer _audioPlayer;
    [SerializeField] private OrbPassiveAttack _orbPassiveAttack;
    [SerializeField] private float _attackTime = 1;
    [SerializeField] private float _attackCooldown = 1.1f;
    
    public SpriteRenderer playerSprite_Top;
    public SpriteRenderer playerSprite_Bottom;
    public BoxCollider2D hitBox;
    public BoxCollider2D projectileDestroyerCollider;
    
    private readonly Vector2 _playerCenterOffset = new (0,0);
    private HashSet<IDamageable> _attackAffected = new HashSet<IDamageable>();
    private Vector2 _moveDirection;
    private float _moveSpeed;
    private bool dealDamage;
    private bool isDead;
    private bool isSkillAvailable = true;
    private bool isAttackAvailable = true;
    private bool isSkill = false;
    private bool isMoving = true;

    private void Awake()
    {
        _moveSpeed = _player.speed;
        projectileDestroyerCollider.enabled = false;
        _player.OnDeath += OnDeath;
        _player.OnLevelSwitch += OnLevelSwitch;
        _orbPassiveAttack.OnEnemyKilled += _player.EnemyKilled;
        SetAttackParticleParameters();
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
        if (isAttackAvailable && !isSkill && !isDead)
        {
            isAttackAvailable = false;
            _animationHandler.PlayAttack();
            SetAttackParticleParameters();
            _attackAffected.Clear();
            Invoke(nameof(EnableAttack), _attackCooldown);
        }
    }

    public void OnSkills(InputAction.CallbackContext context)
    {
        if (isSkillAvailable && !isDead)
        {
            isSkillAvailable = false;
            isSkill = true;
            hitBox.enabled = false;
            
            _moveSpeed = _player.skillSpeed;
            _animationHandler.PlaySkill();
            
            Invoke(nameof(ShowPlayerSprite), 3f);
            Invoke(nameof(CancelSkill), 2f);
            Invoke(nameof(EnableSkills), 6f);
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
        _attackParticle.Play();
        projectileDestroyerCollider.enabled = true;
    }
    
    public void DisableDamageDealer()
    {
        dealDamage = false;
        projectileDestroyerCollider.enabled = false;
    }
    
    private void SetAttackParticleParameters()
    {
        var attackParticleMain = _attackParticle.main;
        attackParticleMain.startLifetimeMultiplier = _attackTime;
        attackParticleMain.startSpeedMultiplier = _player.attackRadius/_attackTime;
        attackParticleMain.startSizeMultiplier = 2*_player.attackRadius * Mathf.Tan((180-_player.attackAngle)/4);
    }

    private void DealDamage(GameObject enemy)
    {
        if (enemy.TryGetComponent(out IDamageable damageable))
        {
            if(!_attackAffected.Contains(damageable))
            {
                _player.EnemyKilled();
                damageable.TakeDamage(_player.attackPower);
                _attackAffected.Add(damageable);
            }
        }
    }
    
    public void DealDamage()
    {
        if(isDead) return;
        Vector2 screenCenter = new Vector2((float)Screen.width/2, (float)Screen.height/2);
        Vector2 direction = (Mouse.current.position.ReadValue() - screenCenter).normalized;
        
        Vector2 attackCenter = (Vector2)transform.position + direction * _player.attackRadius/2;
        projectileDestroyerCollider.transform.position = attackCenter;
        projectileDestroyerCollider.size = new Vector2(_player.attackRadius, _player.attackRadius);
        
        _attackParticle.transform.parent.LookAt2D((Vector2)transform.position + direction);
        
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _player.attackRadius, _player.enemyLayer);
        foreach (var collider in hitColliders)
        {
            Vector2 directionToCollider = collider.transform.position - transform.position + (Vector3)_playerCenterOffset;
            if(Vector2.Angle(directionToCollider, direction) < _player.attackAngle)
            {
                DealDamage(collider.gameObject);
            }
        }
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