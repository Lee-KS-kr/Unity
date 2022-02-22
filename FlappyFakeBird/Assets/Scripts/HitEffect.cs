using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
// Input System

public class HitEffect : MonoBehaviour
{
    // 이펙트를 움직이는 스피드
    public float speed = 10.0f;
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
        if(context.performed)
        {
            moveVector = context.ReadValue<Vector2>();
            Debug.Log(moveVector);
        }
           
            //얘는 멈추는 코드고
        if (context.canceled)
        {
            moveVector = Vector3.zero;
        }
    }

    private void Update()
    {
        transform.Translate((Vector3)moveVector * Time.deltaTime * speed);
    }
}
