using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target = null;
    public float smoothness = 2.0f;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        // 덜그덕거림
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, smoothness * Time.deltaTime);
        // transform.LookAt(target);
        // transform.rotation = target.rotation; // 덜그덕거림
        transform.position = Vector3.Lerp(transform.position, target.position, smoothness * Time.deltaTime);
        // transform.position = Vector3.Lerp(transform.position, target.position+new Vector3(0, 0, -offset), smoothness * Time.deltaTime);
    }
}
