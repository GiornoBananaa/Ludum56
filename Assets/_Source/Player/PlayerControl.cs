using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    public SpriteRenderer playerSprite;
    public BoxCollider2D playerCollider;

    private Vector2 _moveDirection;

    private bool isSkillAvailable = true;
    private bool isAttackAvailable = true;
    private bool isPlayerSpriteVisible = true;
    private bool isMoving = true;

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
        if (isAttackAvailable && isPlayerSpriteVisible)
        {
            isAttackAvailable = false;
            Debug.Log("Attack");
            Invoke(nameof(EnableAttack), 1.57f);
        }
    }

    public void OnSkills(InputAction.CallbackContext context)
    {
        if (isSkillAvailable)
        {
            isSkillAvailable = false;
            isPlayerSpriteVisible = false;
            playerSprite.enabled = false;
            playerCollider.enabled = false;
            isMoving = false;

            _moveSpeed = _moveSpeed + 5;

            Invoke(nameof(ShowPlayerSprite), 3f); // ����� �����������
            Invoke(nameof(EnableSkills), 7f); // ����� �����������
            Invoke(nameof(EnableMovement), 1f);
            Debug.Log("�� ���� DOWN, ��?");
        }
    }

    private void ShowPlayerSprite()
    {
        _moveSpeed = _moveSpeed - 5;

        playerSprite.enabled = true;
        playerCollider.enabled = true;
        isPlayerSpriteVisible = true;
        isMoving = false; // Stop moving
        Invoke(nameof(ResumeMoving), 1f); // Resume moving after 1 second
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


    private void Move(Vector3 direction)
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;

        Vector3 moveDirection = new Vector3(direction.x, direction.y, direction.z);
        transform.position += moveDirection * scaledMoveSpeed;
    }

    private void Update()
    {
        if (isMoving)
        {
            Move(_moveDirection);
        }
    }
}
