using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Variables
    [SerializeField] private float moveSpeed;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector3 moveVector;

    public bool isGameOver = false;
    #endregion

    #region Unity Methods
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Translate(moveVector * moveSpeed * Time.deltaTime);
        if (isGameOver)
        {
            moveVector = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            isGameOver = true;
            animator.SetTrigger("isHit");
        }
    }
    #endregion

    #region Helper Methods
    public void Moving(InputAction.CallbackContext context)
    {
        if (isGameOver) return;
        if (context.performed)
        {
            moveVector = context.ReadValue<Vector2>();
            animator.SetBool("isMoving", true);

            if (moveVector.x == -1)
            {
                spriteRenderer.flipX = true;
            }
            else if (moveVector.x == 1)
            {
                spriteRenderer.flipX = false;
            }
        }
        else if (context.canceled)
        {
            moveVector = Vector2.zero;
            animator.SetBool("isMoving", false);
        }
    }
    #endregion
}
