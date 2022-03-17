using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Input System ������
    #region Variables
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpPower = 5; // �����Ŀ��� ����Ƽ �ֿܼ��� ���氡��
    private float attackDelay = 0.5f;
    private float attackOnDelay = 0;
    private float jumpOffDelay = 1;

    [SerializeField] private Camera playerCamera; // ī�޶� ���� ��ȯ ������
    public GameObject shoot; // �� �������� �� ���� ������Ʈ
    public GameObject stonePrefab; // ���� ������ ��������
    private Rigidbody playerRb;

    private Vector3 lookAtVector; // �ڵ��ƺ��� ���� ������
    private Vector3 moveVector = new Vector3();

    private bool isJumping; // ������ 1ȸ�� ���
    // private bool isOnGround;
    // private bool isLookingBack = false; // �ڵ��ƺ��⸦ �ϰ� �;��� ������ ����
    #endregion

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        // isJumping = false;
    }

    #region Character Move & Jump
    private void Update()
    {
        attackOnDelay += Time.deltaTime;
        transform.Translate(moveVector * walkSpeed * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            //isOnGround = true;
        }
    }

    private void TurnAround()
    {
        Debug.Log("�ڵ���");
        transform.Rotate(0, 180, 0);
    }
    #endregion

    #region Input System Methods
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveVector = context.ReadValue<Vector3>();
            Debug.Log(moveVector.z);
            if (moveVector.z == -1)
            {
                TurnAround();
            }
        }
        else if (context.canceled)
            moveVector = Vector3.zero;
    }

    public void OnJump()
    {
        if (!isJumping)
        {
            // playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // ������ �Ͻ������� �ֵ�����
            // moveVector.y = jumpPower;
            StartCoroutine(Jumping());
            isJumping = true;
        }
    }

    private IEnumerator Jumping()
    {
        moveVector.y = jumpPower;
        yield return new WaitForSeconds(jumpOffDelay);
        moveVector.y = 0f;
    }

    public void OnAttack()
    {
        if (attackOnDelay >= attackDelay)
        {
            transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
            Instantiate(stonePrefab, shoot.transform.position, Camera.main.transform.rotation);
            attackOnDelay = 0;
        }
    }
    #endregion
}