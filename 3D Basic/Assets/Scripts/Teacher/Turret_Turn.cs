using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Turn : Turret_Base
{
    [Header("TurnMode용 변수")] [Range(0, 45)]
    public float halfAngle = 20.0f;
    public float rotateSpeed = 90;
    public float rotateDirection = 1;
    private float _targetAngle = 0;
    
    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        StartFire();
    }

    private void Update()
    {
        TurnMode();
    }

    private void TurnMode()
    {
        if (_guns != null)
        {
            _targetAngle += rotateDirection * rotateSpeed * Time.deltaTime;
            _targetAngle = Mathf.Clamp(_targetAngle, -halfAngle, halfAngle);
    
            if (Mathf.Abs(_targetAngle) >= halfAngle)
            {
                rotateDirection *= -1.0f;
            }
            
            transform.rotation = Quaternion.Euler(0, _targetAngle, 0);
        }
    }
}
