using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpPower = 5; // 점프파워는 유니티 콘솔에서 변경가능
    [SerializeField] private Camera playerCamera; // 카메라 시점 변환 연구중
    public GameObject shoot; // 돌 날려보낼 빈 게임 오브젝트
    public GameObject stonePrefab; // 공격 구현용 돌날리기
    private Rigidbody playerRb;
    private Vector3 lookAtVector; // 뒤돌아보기 구현 연구중
    private Vector3 moveVector = new Vector3();
    private bool isJumping; // 점프는 1회만 허용
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
            playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // 점프만 일시적으로 애드포스
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
