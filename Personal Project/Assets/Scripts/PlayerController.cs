using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Input System 연습용
    #region Variables
    [SerializeField] private float walkSpeed;
    [SerializeField] private float jumpPower = 5; // 점프파워는 유니티 콘솔에서 변경가능
    private float attackDelay = 0.5f;
    private float attackOnDelay = 0;
    private float jumpOffDelay = 1;

    [SerializeField] private Camera playerCamera; // 카메라 시점 변환 연구중
    public GameObject shoot; // 돌 날려보낼 빈 게임 오브젝트
    public GameObject stonePrefab; // 공격 구현용 돌날리기
    private Rigidbody playerRb;

    private Vector3 lookAtVector; // 뒤돌아보기 구현 연구중
    private Vector3 moveVector = new Vector3();

    private bool isJumping; // 점프는 1회만 허용
    // private bool isOnGround;
    // private bool isLookingBack = false; // 뒤돌아보기를 하고 싶었던 사투의 흔적
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
        Debug.Log("뒤돌아");
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
            // playerRb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // 점프만 일시적으로 애드포스
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