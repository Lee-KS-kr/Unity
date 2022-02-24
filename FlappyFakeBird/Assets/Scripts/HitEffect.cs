using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// Input System

public class HitEffect : MonoBehaviour
{
    // ����Ʈ�� �����̴� ���ǵ�
    private float speed = 5.0f;
    private Vector2 moveVector;
    //private Rigidbody2D rigidBody; // �̰� �ʿ��ұ�....?

    //void Awake()
    //{
    //    rigidBody = GetComponent<Rigidbody2D>();
    //}

    // ����Ʈ�� �����̰� �ϴ� �ڵ�
    public void Move(InputAction.CallbackContext context)
    {
        // ������ �����̰� ���� ���߰�
        // �����̴� �ڵ�� ������Ʈ�� ���ְ��ְ� ���� �־���ϰ�
        // �밢������ �����̰� �ϱ� ���ؼ� performed�� ����߽��ϴ�.
        if(context.performed)
        {
            // ��ư�� ������ ���� ���� Vector2�� ����
            moveVector = context.ReadValue<Vector2>();
            //Debug.Log(moveVector);
        }
           
            //��� ���ߴ� �ڵ��
        if (context.canceled)
        {
            // ���߰� �ؾ� �ϱ� ������ Vector3 0,0,0���� ���� ��ȯ
            moveVector = Vector3.zero;
        }
    }

    private void Update()
    {
        // Vector3������ ����� ��ȯ, Update�� �ӵ��� �����ϰ� �ϱ� ���� Time.deltaTime ���.
        transform.Translate((Vector3)moveVector * Time.deltaTime * speed);
    }
}
