using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerController : MonoBehaviour
{
    //과제
    //조건1. 테스트 플레이어는 바닥에 닿으면 죽는다
    //조건2. 테스트 플레이어는 적 새와 부딪히면 축는다
    //조건3. 테스트 플레이어는 죽으면 움직일 수 없다.
    //조건4. 테스트 플레이어는 죽으면 반대쪽 바닥으로 구른다(Z축+)
    //조건4. 테스트 플레이어가 죽어서 바닥에 닿으면 배경 스크롤이 멈춘다.

    public float jumpPower;
    private Rigidbody2D rigidBody;
    public float speed = 10;
    public bool isGameOver;
    public bool isOnGround;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        isGameOver = false;
        isOnGround = false;
    }

    public void JumpUp(InputAction.CallbackContext context)
    {
        // context.started; 키를 눌렀을 때
        // context.performed; 키를 길게 눌렀을 때(챠징 등)
        // context.canceled; 키를 눌렀다가 뗐을 때
        if (context.started)
            rigidBody.AddForce(Vector2.up * jumpPower);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            isGameOver = true;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            isGameOver = true;
        }
    }
}
