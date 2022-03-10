using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpPower = 5; // �����Ŀ��� ����Ƽ �ֿܼ��� ���氡��
    [SerializeField] private Camera playerCamera; // ī�޶� ���� ��ȯ ������
    public GameObject shoot; // �� �������� �� ���� ������Ʈ
    public GameObject stonePrefab; // ���� ������ ��������
    private Rigidbody playerRb;
    private Vector3 lookAtVector; // �ڵ��ƺ��� ���� ������
    private Vector3 moveVector = new Vector3();
    private bool isJumping; // ������ 1ȸ�� ���
    #endregion

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        isJumping = false;
    }

    private void Update()
    {
        transform.Translate(moveVector * walkSpeed * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    #region InputSystemMethods
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
            moveVector = context.ReadValue<Vector3>();
        else if (context.canceled)
            moveVector = Vector3.zero;
    }

    public void OnJump()
    {
        if (!isJumping)
        {
            playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // ������ �Ͻ������� �ֵ�����
            //moveVector = Vector3.up * jumpPower;
            isJumping = true;
        }
    }

    public void OnAttack()
    {
        Instantiate(stonePrefab, shoot.transform.position, Camera.main.transform.rotation);
    }
    #endregion
}
