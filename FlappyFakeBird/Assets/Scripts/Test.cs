using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    Vector2 moveDir = new Vector2();
    Rigidbody2D rigid = null;
    public float moveSpeed = 30.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // InputAction.CallbackContext context
    // �� �Ķ���Ϳ��� �÷��̾ �Է��� ������ ����ִ�(�ش� ���ε��� ���� ������)
    public void MoveInput(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        // Time.deltaTime = ���� Update�κ��� ����� �ð�

        //rigid.AddForce(moveDir * moveSpeed * Time.deltaTime);
        rigid.MovePosition(moveDir); //Translate�� ���
    }
}
