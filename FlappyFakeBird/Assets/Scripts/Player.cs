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
    public GameObject youDied;
    public float speed = 10;
    //������ٵ� ������Ʈ�� �������� ���� ��������
    public float torqueSpeed = 10.0f; // ȸ�� �ӵ�. ������ ȸ�� �ӵ��� unity���� Ȯ���ϸ� �۾��ϱ� ���� public����
    public bool isGameOver = false; // �浹����Ȯ��. �浹�� ���ӿ����� on���� �Ͽ� ������ ���� ����
    public bool isOnGround = false; // �ٴڿ� ��Ҵ��� ���� Ȯ��. player�� �ٴڿ� ������ ��ũ���� �����.

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
        if (context.started && !isGameOver) // enemy�� ground�� ������ ������ ���߰� �Ѵ�.
            rigidBody.AddForce(Vector2.up * jumpPower);
    }

    private void OnCollisionEnter2D(Collision2D other) // collider�浹�� gameover���� ����
    {
        if (other.gameObject.CompareTag("Ground")) // ground�� �浹�Ͽ��� �� 
        {
            isOnGround = true; // ground�� �浹�Ͽ����� true�� �Ͽ� ��ũ���� ����
            isGameOver = true; // ���̻� ������ ���ϵ��� true�� ����
            rigidBody.AddTorque(torqueSpeed); // ȸ����Ŵ 
            rigidBody.AddForce(Vector2.left * speed, ForceMode2D.Impulse); // �������� ����
            youDied.SetActive(true);
            
        }
        else if (other.gameObject.CompareTag("Enemy")) // enemy��ü�� �浹�Ͽ��� ��
        {
            isGameOver = true; // ���̻� ������ ���ϵ��� true�� ����
            rigidBody.AddForce(Vector2.down,ForceMode2D.Impulse); // �ٴڿ� ����������
            // ���� �ٴڿ� ������ 59������ �����
        }
    }
}
