using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerController : MonoBehaviour
{
    //����
    //����1. �׽�Ʈ �÷��̾�� �ٴڿ� ������ �״´�
    //����2. �׽�Ʈ �÷��̾�� �� ���� �ε����� ��´�
    //����3. �׽�Ʈ �÷��̾�� ������ ������ �� ����.
    //����4. �׽�Ʈ �÷��̾�� ������ �ݴ��� �ٴ����� ������(Z��+)
    //����4. �׽�Ʈ �÷��̾ �׾ �ٴڿ� ������ ��� ��ũ���� �����.

    public float jumpPower;
    private Rigidbody2D rigidBody;
    public float speed = 10;
    public bool isGameOver;
    public bool isOnGround;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        isGameOver = false;
        isOnGround = false;
    }

    public void JumpUp(InputAction.CallbackContext context)
    {
        // context.started; Ű�� ������ ��
        // context.performed; Ű�� ��� ������ ��(í¡ ��)
        // context.canceled; Ű�� �����ٰ� ���� ��
        if (context.started)
            rigidBody.AddForce(Vector2.up * jumpPower);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            isGameOver = true;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            isGameOver = true;
        }
    }
}
