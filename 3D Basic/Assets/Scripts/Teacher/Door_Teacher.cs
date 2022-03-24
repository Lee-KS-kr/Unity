using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Door_Teacher : MonoBehaviour
{
    public GameObject door;
    private Animator _animator;
    private static readonly int DoorOpen = Animator.StringToHash("DoorOpen");
    private static readonly int DoorClose = Animator.StringToHash("DoorClose");
    private static readonly int DoorOpenBack = Animator.StringToHash("DoorOpenBack");
    private static readonly int DoorCloseBack = Animator.StringToHash("DoorCloseBack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Open(bool front)
    {
        if(front)
            _animator.SetTrigger(DoorOpen);
        else
        {
            _animator.SetTrigger(DoorOpenBack);
        }
    }

    private void Close()
    {
        _animator.SetTrigger(DoorClose);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool front = CheckFront(other.transform);
            Open(front);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Close();
        }
    }

    private bool CheckFront(Transform target)
    {
        bool result = false;
        Vector3 dir = transform.position - target.position;
        float angle = Vector3.Angle(dir, transform.forward);
        if (angle > 90.0f && angle < 180.0f)
            result = true;
        
        return result;
    }
}
