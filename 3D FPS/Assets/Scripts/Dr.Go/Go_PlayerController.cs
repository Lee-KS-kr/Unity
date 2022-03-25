using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go_PlayerController : MonoBehaviour
{
    private RotateToMouse rotateToMouse; // 마우스 이동으로 카메라 회전

    private void Awake()
    {
        // 마우스 커서를 보이지 않게 설정하고, 현재 위치에 고정시킨다.
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        rotateToMouse = GetComponent<RotateToMouse>();
    }

    private void Update()
    {
        UpdateRotate();
    }

    private void UpdateRotate()
    {
        float mouseX = Input.GetAxis($"MouseX");
        float mouseY = Input.GetAxis($"MouseY");
        
        rotateToMouse.UpdateRotate(mouseX, mouseY);
    }
}

