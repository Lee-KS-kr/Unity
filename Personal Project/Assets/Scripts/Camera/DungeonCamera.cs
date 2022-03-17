using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping = 1.0f;

    private void Start()
    {
        offset = (transform.position - target.transform.position);
    }

    private void LateUpdate()
    {
        float currentAngle = transform.eulerAngles.y;
        float followingAngle = target.transform.eulerAngles.y;
        float angle = Mathf.Lerp(currentAngle, followingAngle, damping * Time.deltaTime);

        Quaternion rotate = Quaternion.Euler(0, angle, 0);
        transform.position = target.transform.position - (rotate * offset);
        transform.LookAt(target.transform);
    }
}
