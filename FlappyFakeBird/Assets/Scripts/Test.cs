using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    Vector2 moveDir = new Vector2();
    Rigidbody2D rigid = null;
    public float moveSpeed = 30.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // InputAction.CallbackContext context
    // 이 파라메터에는 플레이어가 입력한 정보가 들어있다(해당 바인딩에 대한 정보만)
    public void MoveInput(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        // Time.deltaTime = 이전 Update로부터 경과된 시간

        //rigid.AddForce(moveDir * moveSpeed * Time.deltaTime);
        rigid.MovePosition(moveDir); //Translate와 비슷
    }
}
