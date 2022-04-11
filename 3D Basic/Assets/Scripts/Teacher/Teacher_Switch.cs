using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Teacher_Switch : MonoBehaviour, IUseable
{
    public Door targetDoor = null;
    public Transform bar = null;
    private bool _switchOn = false;
    private const float _angle = 15.0f;

    public void OnUse()
    {
        if (_switchOn)
        {
            _switchOn = false;
            bar.rotation = Quaternion.Euler(-_angle, 0, 0);
            _switchOn = targetDoor.Close();  // 연결된 문을 닫았다.
        }
        else
        {
            _switchOn = true;
            bar.rotation = Quaternion.Euler(_angle, 0, 0);
            _switchOn = targetDoor.Open();                   // 연결된 문을 열었다.
        }
    }
}
