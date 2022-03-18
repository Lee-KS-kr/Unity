using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private float spinInput = 0.0f;
    private float moveInput = 0.0f;

    public float moveSpeed = 5.0f;
    public float spinSpeed = 360.0f;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, spinInput * spinSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);
        Vector2 input = context.ReadValue<Vector2>();
        spinInput = input.x;
        moveInput = input.y;

        if (input == Vector2.zero)
            animator.SetBool("isWalking", false);
    }
}
