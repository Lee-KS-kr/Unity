using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// Input System

public class HitEffect : MonoBehaviour
{
    // 이펙트를 움직이는 스피드
    private float speed = 5.0f;
    private Vector2 moveVector;
    //private Rigidbody2D rigidBody; // 이게 필요할까....?

    //void Awake()
    //{
    //    rigidBody = GetComponent<Rigidbody2D>();
    //}

    // 이펙트를 움직이게 하는 코드
    public void Move(InputAction.CallbackContext context)
    {
        // 눌리면 움직이고 떼면 멈추고
        // 움직이는 코드는 업데이트가 해주고있고 값은 넣어야하고
        // 대각선으로 움직이게 하기 위해서 performed를 사용했습니다.
        if(context.performed)
        {
            // 버튼을 누르면 누른 값을 Vector2로 받음
            moveVector = context.ReadValue<Vector2>();
            //Debug.Log(moveVector);
        }
           
            //얘는 멈추는 코드고
        if (context.canceled)
        {
            // 멈추게 해야 하기 때문에 Vector3 0,0,0으로 값을 변환
            moveVector = Vector3.zero;
        }
    }

    private void Update()
    {
        // Vector3형으로 명시적 변환, Update의 속도를 일정하게 하기 위해 Time.deltaTime 사용.
        transform.Translate((Vector3)moveVector * Time.deltaTime * speed);
    }
}
