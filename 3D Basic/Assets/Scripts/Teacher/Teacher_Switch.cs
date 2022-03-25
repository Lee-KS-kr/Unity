using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Teacher_Switch : MonoBehaviour, IUseable
{
    private Transform _bar = null;
    private bool _switchOn = false;
    private  const float _angle=15.0f;

    private void Awake()
    {
        transform.Find("Bar");
    }

    public void OnUse()
    {
        if (_switchOn)
        {
            _switchOn = false;
            _bar.rotation = Quaternion.Euler(-_angle, 0, 0);
        }
        else
        {
            _switchOn = true;
            _bar.rotation = Quaternion.Euler(_angle, 0, 0);
        }
    }
}
