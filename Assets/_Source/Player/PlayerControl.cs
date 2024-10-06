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

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
        Move(_moveDirection);
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

            _moveSpeed = _moveSpeed + 5;

            Invoke(nameof(ShowPlayerSprite), 3f); // Время невидимости
            Invoke(nameof(EnableSkills), 7f); // Время перезарядки
            Debug.Log("Ну типо DOWN, ок?");
        }
    }

    private void ShowPlayerSprite()
    {
        _moveSpeed = _moveSpeed - 5;

        playerSprite.enabled = true;
        playerCollider.enabled = true;
        isPlayerSpriteVisible = true;
    }

    private void EnableAttack()
    {
        isAttackAvailable = true;
    }

    private void EnableSkills()
    {
        isSkillAvailable = true;
    }

    private void Move(Vector3 direction)
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;

        Vector3 moveDirection = new Vector3(direction.x, direction.y, direction.z);
        transform.position += moveDirection * scaledMoveSpeed;
    }

    private void Update()
    {
        Move(_moveDirection);
    }
}
