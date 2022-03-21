using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// [RequireComponent(typeof(Animator))] 이 스크립트를 가진 게임 오브젝트는 무조건 괄호속 컴포넌트를 가진다
// 없으면 만들어서라도 가진다.
public class Player : MonoBehaviour
{
    private float spinInput = 0.0f; // 회전 입력 여부(-1.0 ~ 1.0)
    private float moveInput = 0.0f;  // 이동 입력 여부(-1.0 ~ 1.0)

    public float moveSpeed = 5.0f; // 플레이어 이동속도(기본값 1초에 5)
    public float spinSpeed = 360.0f; // 플레이어 회전속도(기본값 1초에 한바퀴)

    private Animator animator; // 애니메이터 컴포넌트
    private TeacherController playerControl; // 입력처리용 클래스
    private Rigidbody playerRigidbody;

    // 오브젝트가 생성된 직후에 1회 실행
    private void Awake()
    {
        // 오브젝트에 있는 애니메이터 컴포넌트를 찾아서 캐싱
        animator = GetComponent<Animator>();
        playerControl = new TeacherController(); // Input Action Asset을 이용해 자동 생성한 클래스
        playerControl.Player.UseItem.started += UseItem;
        // Player라는 액션맵에 있는 UseItem액션이 started일 때 UseItem함수를 실행하도록 바인딩
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // 게임 오브젝트가 활성화될 때 실행
    private void OnEnable()
    {
        playerControl.Player.Enable(); // 액션맵도 함께 활성화
    }

    // 게임 오브젝트가 비활성화 될 때 실행
    private void OnDisable()
    {
        playerControl.Player.Disable(); // 액션맵도 함께 비활성화
    }

    // 매 프레임 호출
    //private void Update()
    //{
    //    transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);
    //    transform.Rotate(Vector3.up, spinInput * spinSpeed * Time.deltaTime);
    //}

    private void FixedUpdate()
    {
        // 현재 위치 + 캐릭터가 바라보는 방향으로 1초에 moveSpeed씩 이동
        playerRigidbody.MovePosition(playerRigidbody.position + transform.forward * moveInput * moveSpeed * Time.fixedDeltaTime);
        // 현재 각도 + 추가각도
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.AngleAxis(spinInput * spinSpeed * Time.fixedDeltaTime, Vector3.up));
    }

    public void Move(InputAction.CallbackContext context)
    {
        //animator.SetBool("isWalking", true);
        Vector2 input = context.ReadValue<Vector2>();
        spinInput = input.x;
        moveInput = input.y;
        if (context.started)
        {
            animator.SetBool("isMove", true);
        }
        if (context.canceled)
        {
            animator.SetBool("isMove", false);
        }
        //if (input == Vector2.zero)
        //    animator.SetBool("isWalking", false);
    }

    public void UseItem(InputAction.CallbackContext context)
    {
        animator.SetTrigger("OnUseItem"); // 스페이스 키를 눌렀을 때 트리거 실행
    }
}
