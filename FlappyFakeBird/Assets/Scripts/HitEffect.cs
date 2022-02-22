using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// Input System

public class HitEffect : MonoBehaviour
{
    // ����Ʈ�� �����̴� ���ǵ�
    public float speed = 10.0f;
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
        if(context.performed)
        {
            moveVector = context.ReadValue<Vector2>();
            Debug.Log(moveVector);
        }
           
            //��� ���ߴ� �ڵ��
        if (context.canceled)
        {
            moveVector = Vector3.zero;
        }
    }

    private void Update()
    {
        transform.Translate((Vector3)moveVector * Time.deltaTime * speed);
    }
}
