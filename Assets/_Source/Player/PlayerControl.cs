using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    public SpriteRenderer playerSprite;

    private Vector2 _moveDirection;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveDirection = context.ReadValue<Vector2>();
        Move(_moveDirection);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack");
    }

    public void OnSkills(InputAction.CallbackContext context)
    {
        playerSprite.enabled = false;
        Invoke(nameof(ShowPlayerSprite), 3f);

        Debug.Log("Ну типо DOWN, ок?");
    }

    private void ShowPlayerSprite()
    {
        playerSprite.enabled = true;
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
