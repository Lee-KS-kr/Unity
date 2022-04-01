using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Stay : Turret_Base
{
    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        StartFire();
    }
}
