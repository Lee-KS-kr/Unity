using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretMode
{
    Stay = 0,
    Turn,
    Trace,
}

public class Turret_Switch : Turret_Base
{
    private TurretMode _turretMode;
    public float changeModeInterval = 5.0f;
    private IEnumerator saveShot;
    
    [Header("TurnMode용 변수")] [Range(0, 45)]
    public float halfAngle = 20.0f;
    public float rotateSpeed = 90;
    public float rotateDirection = 1;
    private float _targetAngle = 0;
    
    [Header("TraceMode용 변수")] [Range(5, 20)]
    public float smoothness = 3.0f;
    public float fireAngle = 5;
    public Transform lookAtTarget; // 플레이어

    private void Awake()
    {
        saveShot = SwithMode();
        Initialize();
        StayMode();
        StartCoroutine(saveShot);
    }
    
    private void Update()
    {
        switch (_turretMode)
        {
            case TurretMode.Stay:
                StayMode();
                break;
            case TurretMode.Turn:
                TurnMode();
                break;
            case TurretMode.Trace:
                TraceMode();
                break;
        }
    }

    private void StayMode()
    {
        StartFire();
    }
    
    private void TurnMode()
    {
        if (_guns != null)
        {
            _targetAngle += rotateDirection * rotateSpeed * Time.deltaTime;
            _targetAngle = Mathf.Clamp(_targetAngle, -halfAngle, halfAngle);
    
            if (Mathf.Abs(_targetAngle) >= halfAngle)
            {
                rotateDirection *= -1.0f;
            }
            
            transform.rotation = Quaternion.Euler(0, _targetAngle, 0);
        }
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
            _turretMode = TurretMode.Trace;
            StopCoroutine(saveShot);
            lookAtTarget = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(saveShot);
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
        float angle = Vector3.Angle(transform.forward, lookAtTarget.position - transform.position);
        return Mathf.Abs(angle) < fireAngle;
    }

    private IEnumerator SwithMode()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeModeInterval);
            int modeNumber = Random.Range(0, 2); // 이걸 어떻게 깔끔하게 바꿀 수 있을까?
            if (modeNumber == 0) _turretMode = TurretMode.Stay;
            else _turretMode = TurretMode.Turn;
        }
    }
}
