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
        Debug.Log($"{other.gameObject.name}가 함정을 밟았다!");
        animator.SetTrigger("TrapActive");
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetTrigger("TrapDeactivate");
    }
}
