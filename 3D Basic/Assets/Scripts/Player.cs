using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// [RequireComponent(typeof(Animator))] �� ��ũ��Ʈ�� ���� ���� ������Ʈ�� ������ ��ȣ�� ������Ʈ�� ������
// ������ ������ ������.
public class Player : MonoBehaviour, IDead
{
    private float _spinInput = 0.0f; // ȸ�� �Է� ����(-1.0 ~ 1.0)
    private float _moveInput = 0.0f;  // �̵� �Է� ����(-1.0 ~ 1.0)

    public float moveSpeed = 5.0f; // �÷��̾� �̵��ӵ�(�⺻�� 1�ʿ� 5)
    public float spinSpeed = 360.0f; // �÷��̾� ȸ���ӵ�(�⺻�� 1�ʿ� �ѹ���)

    private bool _isDead = false; // �÷��̾� �׾���? �ƴϿ�

    private Animator _animator; // �ִϸ����� ������Ʈ
    private TeacherController _playerControl; // �Է�ó���� Ŭ����
    private Rigidbody _playerRigidbody;

    #region Unity Methods
    // ������Ʈ�� ������ ���Ŀ� 1ȸ ����
    private void Awake()
    {
        // ������Ʈ�� �ִ� �ִϸ����� ������Ʈ�� ã�Ƽ� ĳ��
        _animator = GetComponent<Animator>();
        _playerControl = new TeacherController(); // Input Action Asset�� �̿��� �ڵ� ������ Ŭ����
        _playerControl.Player.UseItem.started += UseItem;
        // Player��� �׼Ǹʿ� �ִ� UseItem�׼��� started�� �� UseItem�Լ��� �����ϵ��� ���ε�
        _playerRigidbody = GetComponent<Rigidbody>();
    }

    // ���� ������Ʈ�� Ȱ��ȭ�� �� ����
    private void OnEnable()
    {
        _playerControl.Player.Enable(); // �׼Ǹʵ� �Բ� Ȱ��ȭ
    }

    // ���� ������Ʈ�� ��Ȱ��ȭ �� �� ����
    private void OnDisable()
    {
        _playerControl.Player.Disable(); // �׼Ǹʵ� �Բ� ��Ȱ��ȭ
    }

    // �� ������ ȣ��
    //private void Update()
    //{
    //    transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);
    //    transform.Rotate(Vector3.up, spinInput * spinSpeed * Time.deltaTime);
    //}

    private void FixedUpdate()
    {
        if (!_isDead) // �÷��̾ �׾��� ���� �����̰� �ϵ���
        {
            // ���� ��ġ + ĳ���Ͱ� �ٶ󺸴� �������� 1�ʿ� moveSpeed�� �̵�
            _playerRigidbody.MovePosition(_playerRigidbody.position + transform.forward * _moveInput * moveSpeed * Time.fixedDeltaTime);
            // ���� ���� + �߰�����
            _playerRigidbody.MoveRotation(_playerRigidbody.rotation * Quaternion.AngleAxis(_spinInput * spinSpeed * Time.fixedDeltaTime, Vector3.up));
        }
    }
    #endregion

    #region Input System Methods
    public void Move(InputAction.CallbackContext context)
    {
        if (_isDead) return;
        //animator.SetBool("isWalking", true);
        Vector2 input = context.ReadValue<Vector2>();
        _spinInput = input.x;
        _moveInput = input.y;
        if (context.started)
        {
            _animator.SetBool("isMove", true);
        }
        if (context.canceled)
        {
            _animator.SetBool("isMove", false);
        }
        //if (input == Vector2.zero)
        //    animator.SetBool("isWalking", false);
    }

    public void UseItem(InputAction.CallbackContext context)
    {
        _animator.SetTrigger("OnUseItem"); // �����̽� Ű�� ������ �� Ʈ���� ����
    }
    #endregion

    public void OnDead()
    {
        Debug.Log($"�÷��̾� ���.");
        // ��� ����
        // �ߺ���� ����
        // �׾��� �� �̵�ó�� ����

        _isDead = true; // �÷��̾� ���ó�� �Ͽ� ���̻� Ű�������� �������� �Ұ����ϵ���
        //playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.AngleAxis(-90 , Vector3.right)); // �÷��̾� �Ѿ�߸���
        //Vector3 deadPosition = new Vector3(transform.position.z, -0.6f, transform.position.z); // �״�� ����� ���߿� ���� �ٴڿ� ���� ��ġ ����
        //transform.Translate(deadPosition); // �÷��̾� �ٴڿ� ������

        // rigidbody.MovePosition���� �Ϸ��� �Ͽ����� ��°������ �������� �ʾ� ����ϴٰ� ������ ����
        //Vector3 deadBodyPosition = new Vector3(0, -0.6f, 0);
        //playerRigidbody.MovePosition(playerRigidbody.position + transform.up * -0.6f);

        _animator.SetTrigger("OnDead");
        _playerRigidbody.constraints = RigidbodyConstraints.None;
        _playerRigidbody.drag = 0;
        _playerRigidbody.angularDrag = 0.05f;
        _playerRigidbody.AddForceAtPosition(-transform.forward*10, transform.position + new Vector3(0, 1.5f, 0));
    }
}
