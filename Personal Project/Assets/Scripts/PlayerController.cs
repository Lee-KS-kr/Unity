using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Input System ¿¬½À¿ë

    public float speed = 10;
    public float jumpPower = 5;
    private Vector3 inputVector;

    private void Update()
    {
        transform.Translate(inputVector * speed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputVector = context.ReadValue<Vector3>();
        }
        if(context.canceled)
        {
            inputVector = Vector3.zero;
        }
    }

    public void JumpUp(InputAction.CallbackContext context)
    {

    }
}