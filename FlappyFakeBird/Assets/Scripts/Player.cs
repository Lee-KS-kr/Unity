// 네임스페이스 설정
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Unity 새 Input system을 사용하기 위한 네임스페이스

//접근제한자 : public, private, protected
//public : 누구나 사용 가능
//private : 가지고 있는 자만 사용 가능
//protected : 나와 나를 상속받은 자만 사용가능
//키워드 : C#에서 사용을 예약해 놓은 단어들
//class : 어떤 기능을 하고 어떤 데이터를 가질 수 있는지 설정해 놓은 일종의 설계도, 틀.
//객체 : 클래스를 인스턴스화(실체화, 메모리에 실제로 존재하는 것)한 것
//변수 : 데이터를 저장하는 곳. 데이터 타입을 가진다.
public class Player : MonoBehaviour // MonoBehaniour에서 상속받은 플레이어 클래스
{
    public float jumpPower; 
    //점프파워를 콘솔에서 설정할 수 있도록 public으로 변수선언 지정되는 초기값은 10.0이다.
    private Rigidbody2D rigidBody;
    public GameObject youDied;
    public float speed = 10;
    //리지드바디 컴포넌트를 가져오기 위한 변수선언
    public float torqueSpeed = 10.0f; // 회전 속도. 적당한 회전 속도를 unity에서 확인하며 작업하기 위해 public선언
    public bool isGameOver = false; // 충돌여부확인. 충돌시 게임오버를 on으로 하여 조작을 막는 역할
    public bool isOnGround = false; // 바닥에 닿았는지 여부 확인. player가 바닥에 닿으면 스크롤을 멈춘다.

    // 게임 오브젝트가 만들어진(인스턴스) 직후 실행되는 함수
    private void Awake()
    {
        // 변수에 리지드바디 컴포넌트 초기화
        // 캐싱(미리 찾아서 메모리에 저장해두는 것)을 통해 무거운 작업을 최소화하기 위해 Awake에서 찾음
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // 매 프레임마다 호출되는 함수 최초의 Update가 실행되기 직전에 Start가 호출된다.
    //void Update()
    //{
    //    // Input Manager를 통해 입력처리를 하는 방법
    //    // 스페이스바를 누르면 점프파워만큼 y좌표 위로 올라가도록 설정
    //    if (Input.GetButton("Jump"))
    //    {
    //        rigidBody.AddForce(Vector2.up * jumpPower);
    //    }
    //}

    // 스페이스 버튼을 눌렀을 때 실행될 함수
    // 기능은 rigidbody를 통해서 y축 +로 힘을 더한다.

    public void JumpUp(InputAction.CallbackContext context)
    {
        // context.started; 키를 눌렀을 때
        // context.performed; 키를 길게 눌렀을 때(챠징 등)
        // context.canceled; 키를 눌렀다가 뗐을 때
        if (context.started && !isGameOver) // enemy나 ground에 닿으면 조작을 멈추게 한다.
            rigidBody.AddForce(Vector2.up * jumpPower);
    }

    private void OnCollisionEnter2D(Collision2D other) // collider충돌로 gameover여부 측정
    {
        if (other.gameObject.CompareTag("Ground")) // ground와 충돌하였을 때 
        {
            isOnGround = true; // ground와 충돌하였음을 true로 하여 스크롤을 중지
            isGameOver = true; // 더이상 조작을 못하도록 true로 설정
            rigidBody.AddTorque(torqueSpeed); // 회전시킴 
            rigidBody.AddForce(Vector2.left * speed, ForceMode2D.Impulse); // 왼쪽으로 보냄
            youDied.SetActive(true);
            
        }
        else if (other.gameObject.CompareTag("Enemy")) // enemy개체와 충돌하였을 때
        {
            isGameOver = true; // 더이상 조작을 못하도록 true로 설정
            rigidBody.AddForce(Vector2.down,ForceMode2D.Impulse); // 바닥에 떨어지도록
            // 이후 바닥에 닿으면 59번줄이 실행됨
        }
    }
}
