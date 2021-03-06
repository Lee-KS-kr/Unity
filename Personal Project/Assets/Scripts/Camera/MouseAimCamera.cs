using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseAimCamera : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float rotateSpeed = 5;
    private Vector3 offset;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void LateUpdate()
    {
        float horizontal = Mouse.current.delta.x.ReadValue() * rotateSpeed;
        //float vertical = Mouse.current.delta.y.ReadValue() * rotateSpeed;
        target.transform.Rotate(0, horizontal, 0);

        //float desiredAngleX = target.transform.eulerAngles.x;
        float desiredAngleY = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngleY, 0);
        transform.position = target.transform.position - (rotation * offset);

        transform.LookAt(target.transform);
    }
}
