using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Trace : Turret_Base
{
    [Header("TraceMode용 변수")] [Range(5, 20)]
    public float lookAtRange = 5; // 플레이어를 인식할 범위
    public float smoothness = 3.0f;
    public float fireAngle = 5;
    public Transform lookAtTarget; // 플레이어
    
    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        TraceMode();
    }

    private void TraceMode()
    {
        if (lookAtTarget != null) // 무언가가 트리거 안에 들어와 있다
        {
            LookTarget();
            if (CanShot())
            {
                StartFire();
            }
            else // 쏘는 각도 밖이다
            {
                StopFire();
            }
        }
        else
        {
            StopFire();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lookAtTarget = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lookAtTarget = null;
        }
    }

    private void LookTarget()
    {
        Vector3 dir = lookAtTarget.position - transform.position; // 방향벡터 계산 (터렛->플레이어)
        dir.y = 0;
        transform.rotation =
            Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), smoothness * Time.deltaTime);
        // 시작 회전          // 끝났을 때 회전 상태            // 시작과 끝 사이 지점
    }

    private bool CanShot()
    {
        float angle = Vector3.Angle(_guns[0].transform.forward, lookAtTarget.position - transform.position);
        return Mathf.Abs(angle) < fireAngle;
    }
    
}
