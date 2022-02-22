// ���ӽ����̽� ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Unity �� Input system�� ����ϱ� ���� ���ӽ����̽�

//���������� : public, private, protected
//public : ������ ��� ����
//private : ������ �ִ� �ڸ� ��� ����
//protected : ���� ���� ��ӹ��� �ڸ� ��밡��
//Ű���� : C#���� ����� ������ ���� �ܾ��
//class : � ����� �ϰ� � �����͸� ���� �� �ִ��� ������ ���� ������ ���赵, Ʋ.
//��ü : Ŭ������ �ν��Ͻ�ȭ(��üȭ, �޸𸮿� ������ �����ϴ� ��)�� ��
//���� : �����͸� �����ϴ� ��. ������ Ÿ���� ������.
public class Player : MonoBehaviour // MonoBehaniour���� ��ӹ��� �÷��̾� Ŭ����
{
    public float jumpPower; 
    //�����Ŀ��� �ֿܼ��� ������ �� �ֵ��� public���� �������� �����Ǵ� �ʱⰪ�� 10.0�̴�.
    private Rigidbody2D rigidBody;
    public float speed = 10;
    //������ٵ� ������Ʈ�� �������� ���� ��������

    // ���� ������Ʈ�� �������(�ν��Ͻ�) ���� ����Ǵ� �Լ�
    private void Awake()
    {
        // ������ ������ٵ� ������Ʈ �ʱ�ȭ
        // ĳ��(�̸� ã�Ƽ� �޸𸮿� �����صδ� ��)�� ���� ���ſ� �۾��� �ּ�ȭ�ϱ� ���� Awake���� ã��
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // �� �����Ӹ��� ȣ��Ǵ� �Լ� ������ Update�� ����Ǳ� ������ Start�� ȣ��ȴ�.
    //void Update()
    //{
    //    // Input Manager�� ���� �Է�ó���� �ϴ� ���
    //    // �����̽��ٸ� ������ �����Ŀ���ŭ y��ǥ ���� �ö󰡵��� ����
    //    if (Input.GetButton("Jump"))
    //    {
    //        rigidBody.AddForce(Vector2.up * jumpPower);
    //    }
    //}

    // �����̽� ��ư�� ������ �� ����� �Լ�
    // ����� rigidbody�� ���ؼ� y�� +�� ���� ���Ѵ�.

    public void JumpUp(InputAction.CallbackContext context)
    {
        // context.started; Ű�� ������ ��
        // context.performed; Ű�� ��� ������ ��(í¡ ��)
        // context.canceled; Ű�� �����ٰ� ���� ��
        if(context.started)
            rigidBody.AddForce(Vector2.up * jumpPower);
    }
}
