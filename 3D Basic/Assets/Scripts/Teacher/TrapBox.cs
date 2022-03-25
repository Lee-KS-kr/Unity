using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBox : MonoBehaviour, IUseable
{
    public ParticleSystem effect = null;
    public Light _light = null;
    private IDead _target = null;

    private void OnEnable()
    {
        _light.enabled = false;
    }

    private void OnDisable()
    {
        _light.enabled = false;
    }

    public void OnUse()
    {
        if (effect != null && _light != null)
        {
            effect.Play();
            _light.enabled = true;
        }

        if (_target != null)
        {
            _target.OnDead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        _target = other.gameObject.GetComponent<IDead>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (_target == other.gameObject.GetComponent<IDead>())
        {
            _target = null;
        }
    }
}
