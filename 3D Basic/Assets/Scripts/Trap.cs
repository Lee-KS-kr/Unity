using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.gameObject.name}�� ������ ��Ҵ�!");
        animator.SetTrigger("TrapActive");
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetTrigger("TrapDeactivate");
    }
}
