using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestCamera : MonoBehaviour
{
    // 카메라 한계
    [SerializeField] private float cameraRotationLimit;
    private float currentCameraRotationX;
    private float currentCameraRotationY;
    [SerializeField] private float lookSensitivity;
    private float _xRotation;
    private float _yRotation;

    public void CameraRotation()
    {
        _xRotation = Mouse.current.delta.x.ReadValue() * lookSensitivity;
        _yRotation = Mouse.current.delta.y.ReadValue() * lookSensitivity;
        // float _cameraRotationX = _xRotation ;

        currentCameraRotationX -= _xRotation;
        currentCameraRotationY -= _yRotation;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        currentCameraRotationY = Mathf.Clamp(currentCameraRotationY, -cameraRotationLimit, cameraRotationLimit);

        transform.localEulerAngles = new Vector3(currentCameraRotationX, currentCameraRotationY, 0f);
    }
}
