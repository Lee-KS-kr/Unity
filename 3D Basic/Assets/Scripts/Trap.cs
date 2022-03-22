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
        if(other.CompareTag("Player"))
            animator.SetTrigger("TrapActive");

        IDead dead = other.gameObject.GetComponent<IDead>();
        if (dead != null)
            dead.OnDead();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            animator.SetTrigger("TrapDeactivate");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
