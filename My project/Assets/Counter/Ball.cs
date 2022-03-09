using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private FireBall fireBall;
    private Rigidbody ballRb;

    private void Awake()
    {
        ballRb = GetComponent<Rigidbody>();
        fireBall = FindObjectOfType<FireBall>();
    }

    private void OnEnable()
    {
        ballRb.AddRelativeForce(new Vector3(-1,1,0) * fireBall.chargeStrength, ForceMode.Impulse);
        Invoke("DestroyBall", 5.0f);
    }

    private void DestroyBall()
    {
        Destroy(gameObject);
    }
}
