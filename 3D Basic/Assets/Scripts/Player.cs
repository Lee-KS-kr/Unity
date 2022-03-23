using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// [RequireComponent(typeof(Animator))] 이 스크립트를 가진 게임 오브젝트는 무조건 괄호속 컴포넌트를 가진다
// 없으면 만들어서라도 가진다.
public class Player : MonoBehaviour, IDead
{
#region Variables
    private float _spinInput = 0.0f; // 회전 입력 여부(-1.0 ~ 1.0)
    private float _moveInput = 0.0f;  // 이동 입력 여부(-1.0 ~ 1.0)

    public float moveSpeed = 5.0f; // 플레이어 이동속도(기본값 1초에 5)
    public float spinSpeed = 360.0f; // 플레이어 회전속도(기본값 1초에 한바퀴)

    private bool _isDead = false; // 플레이어 죽었니? 아니요

    private Animator _animator; // 애니메이터 컴포넌트
    private TeacherController _playerControl; // 입력처리용 클래스
    private Rigidbody _playerRigidbody;
    private DoorController _doorController;
    
    private static readonly int Dead = Animator.StringToHash("OnDead");
    private static readonly int OnUseItem = Animator.StringToHash("OnUseItem");
    private static readonly int IsMove = Animator.StringToHash("isMove");
#endregion

    #region Unity Methods
    // 오브젝트가 생성된 직후에 1회 실행
    private void Awake()
    {
        // 오브젝트에 있는 애니메이터 컴포넌트를 찾아서 캐싱
        _animator = GetComponent<Animator>();
        _playerControl = new TeacherController(); // Input Action Asset을 이용해 자동 생성한 클래스
        _playerControl.Player.UseItem.started += UseItem;
        // Player라는 액션맵에 있는 UseItem액션이 started일 때 UseItem함수를 실행하도록 바인딩
        _playerRigidbody = GetComponent<Rigidbody>();
        _doorController = FindObjectOfType<DoorController>();
    }

    // 게임 오브젝트가 활성화될 때 실행
    private void OnEnable()
    {
        _playerControl.Player.Enable(); // 액션맵도 함께 활성화
    }

    // 게임 오브젝트가 비활성화 될 때 실행
    private void OnDisable()
    {
        _playerControl.Player.Disable(); // 액션맵도 함께 비활성화
    }

    // 매 프레임 호출
    //private void Update()
    //{
    //    transform.Translate(Vector3.forward * moveInput * moveSpeed * Time.deltaTime);
    //    transform.Rotate(Vector3.up, spinInput * spinSpeed * Time.deltaTime);
    //}

    private void FixedUpdate()
    {
        if (!_isDead) // 플레이어가 죽었을 때만 움직이게 하도록
        {
            // 현재 위치 + 캐릭터가 바라보는 방향으로 1초에 moveSpeed씩 이동
            _playerRigidbody.MovePosition(_playerRigidbody.position + transform.forward * _moveInput * moveSpeed * Time.fixedDeltaTime);
            // 현재 각도 + 추가각도
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
            _animator.SetBool(IsMove, true);
        }
        if (context.canceled)
        {
            _animator.SetBool(IsMove, false);
        }
        //if (input == Vector2.zero)
        //    animator.SetBool("isWalking", false);
    }

    public void UseItem(InputAction.CallbackContext context)
    {
        if (context.canceled) // started 사용시 이중호출되는 버그가 있어 canceled 사용
        {
            _animator.SetTrigger(OnUseItem); // 스페이스 키를 눌렀을 때 트리거 실행
            if (_doorController.isInCollider) // 콜라이더 내부에 있을때만 활성화
            {
                _doorController.isDoorOpen = !_doorController.isDoorOpen; // 문을 열고 닫도록 bool변수 반전
                _doorController.MoveDoor(); // 문 여닫기 실행
            }
        }
    }
    #endregion

    public void OnDead()
    {
        Debug.Log($"플레이어 사망.");
        // 사망 연출
        // 중복사망 방지
        // 죽었을 때 이동처리 안함

        _isDead = true; // 플레이어 사망처리 하여 더이상 키조작으로 움직임이 불가능하도록
        //playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.AngleAxis(-90 , Vector3.right)); // 플레이어 넘어뜨리기
        //Vector3 deadPosition = new Vector3(transform.position.z, -0.6f, transform.position.z); // 그대로 누우면 공중에 떠서 바닥에 눕힐 위치 지정
        //transform.Translate(deadPosition); // 플레이어 바닥에 눕히기

        // rigidbody.MovePosition으로 하려고 하였으나 어째서인지 움직이지 않아 노력하다가 실패한 흔적
        //Vector3 deadBodyPosition = new Vector3(0, -0.6f, 0);
        //playerRigidbody.MovePosition(playerRigidbody.position + transform.up * -0.6f);

        _animator.SetTrigger(Dead);
        _playerRigidbody.constraints = RigidbodyConstraints.None;
        _playerRigidbody.drag = 0;
        _playerRigidbody.angularDrag = 0.05f;
        var transform1 = transform;
        _playerRigidbody.AddForceAtPosition(-transform1.forward*10, transform1.position + new Vector3(0, 1.5f, 0));
    }
}
