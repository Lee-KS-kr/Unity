using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum TurretMode
{
    Stay = 0,
    Turn,
    Trace,
}

public class Turret : MonoBehaviour
{
    private Gun[] _guns;
    public TurretMode turretMode = TurretMode.Stay;
    public Transform lookAtTarget; // 플레이어

    [Header("Gun 생성용 변수")] [Range(1, 7)] public int gunCount = 0; // 발사할 총구의 갯수
    public GameObject gunPrefab; // 생성할 총구의 프리팹
    private Vector3 _standardPosition = new Vector3(0, 1.5f, 0);
    private float _gunsDistance = 0.3f;

    [Header("TurnMode용 변수")] [Range(0, 45)]
    public float halfAngle = 20.0f;

    public float rotateSpeed = 90;
    public float rotateDirection = 1;
    private float _targetAngle = 0;

    [Header("TraceMode용 변수")] [Range(5, 20)]
    public float lookAtRange = 5; // 플레이어를 인식할 범위

    public float smoothness = 3.0f;
    public float fireAngle = 5;

    private void Awake()
    {
        StartCoroutine(IntantiateGuns());
        _guns = transform.GetComponentsInChildren<Gun>();
    }

    private void Start()
    {
        InitializeGun(1, 3, 0.1f);
        switch (turretMode)
        {
            case TurretMode.Stay:
            case TurretMode.Turn:
                StartFire();
                break;
            case TurretMode.Trace:
                break;
        }
    }

    private void InitializeGun(float interval, int shots, float rateOfFire)
    {
        for (int i = 0; i < _guns.Length; i++)
        {
            _guns[i].Initialize(interval, shots, rateOfFire);
        }
    }

    private void Update()
    {
        switch (turretMode)
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
        // 하는 일 없음
    }

    private void TurnMode()
    {
        if (_guns != null) // 타겟을 바라보는 동안에는 회전하지 않도록 조건 추가 && !_isLookingAt
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
            lookAtTarget = other.transform;
            Debug.Log($"{lookAtTarget.gameObject.name} 2");
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
        float angle = Vector3.Angle(transform.forward, lookAtTarget.position - transform.position);
        return Mathf.Abs(angle) < fireAngle;
    }

    public void StartFire()
    {
        for (int i = 0; i < _guns.Length; i++)
        {
            _guns[i].StartFire();
        }
    }

    public void StopFire()
    {
        for (int i = 0; i < _guns.Length; i++)
        {
            _guns[i].StopFire();
        }
    }

    private IEnumerator IntantiateGuns()
    {
        for (int i = 0; i < gunCount; i++)
        {
            GameObject gunObject = Instantiate(gunPrefab, _standardPosition, Quaternion.identity, transform);
            switch (i)
            {
                case 0:
                    break;
                case 1:
                    gunObject.transform.Translate(_standardPosition.x - _gunsDistance, 0, 0);
                    break;
                case 2:
                    gunObject.transform.Translate(_standardPosition.x + _gunsDistance, 0, 0);
                    break;
                case 3:
                    gunObject.transform.Translate(_standardPosition.x - (_gunsDistance / 2), _gunsDistance, 0);
                    break;
                case 4:
                    gunObject.transform.Translate(_standardPosition.x + (_gunsDistance / 2), _gunsDistance, 0);
                    break;
                case 5:
                    gunObject.transform.Translate(_standardPosition.x - (_gunsDistance / 2), -_gunsDistance, 0);
                    break;
                case 6:
                    gunObject.transform.Translate(_standardPosition.x + (_gunsDistance / 2), -_gunsDistance, 0);
                    break;
            }
        }
        yield break;
    }
}