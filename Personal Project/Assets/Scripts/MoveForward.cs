using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float forwardMoveSpeed = 5;

    private float destroyTime = 3f;

    private void Update()
    {
        transform.Translate(Vector3.forward * forwardMoveSpeed * Time.deltaTime);
    }
}
