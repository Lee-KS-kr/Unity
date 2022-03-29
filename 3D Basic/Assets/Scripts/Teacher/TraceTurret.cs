using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class TraceTurret : MonoBehaviour
{
    public GameObject bullet; // 발사할 총알 오브젝트
    public Transform spawner; // 총알이 발사될 위치
    public Transform pillar;
    private Transform _target;

    public float interval = 1.0f; // 총알을 발사하는 간격(방아쇠를 당기는 간격)
    public float rateOfFire = 0.1f; // 연사 간격
    public int shots = 5; // 한번 발사할 때 몇연사를 할 것인가
    public float fireAngle = 5;

    private IEnumerator _shotSave; // 코루틴용 IEnumerator 저장
    private bool _startedShoot = false;

    // 첫번째 update가 실행되기 직전
    private void Start()
    {
        _shotSave = Shot(); // IEnumerator 저장
        // StartCoroutine(_shotSave); // 저장한 IEnumerator로 코루틴 실행
    }

    private void Update()
    {
        if (_target != null) // 무언가가 트리거 안에 들어와 있다
        {
            LookTarget();
            if (CanShot())
            {
                if (!_startedShoot)
                {
                    StartCoroutine(_shotSave);
                    _startedShoot = true;
                }
            }
            else // 쏘는 각도 밖이다
            {
                if (_startedShoot) // 지금 쏘는 중
                {
                    StopShoot(); // 발사 중지
                }
            }
        }
    }

    private bool CanShot()
    {
        float angle = Vector3.Angle(spawner.forward, _target.position - transform.position);
        return Mathf.Abs(angle) < fireAngle;
    }

    private void LookTarget()
    {
        // 1. LookAt 해당 트랜스폼이 목표를 바라보는 방향으로 회전시킴
        // transform.LookAt(target);

        // 2. LookRotation
        // 특정 방향을 향하는 회전을 생성
        // Vector3 dir = target.position - transform.position;
        // transform.rotation = Quaternion.LookRotation(dir);

        // 3. Quaternion.Lerp, Quaternion.Slerp
        // 보간 : 중간값을 계산
        // 보간을 이용해서 부드럽게 움직이는 연출이 가능
        Vector3 dir = _target.position - transform.position; // 방향벡터 계산 (터렛->플레이어)
        dir.y = 0;
        transform.rotation =
            Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 3.0f * Time.deltaTime);
        // 시작 회전          // 끝났을 때 회전 상태            // 시작과 끝 사이 지점
    }

    // 총알 발사 메소드
    private IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval - shots * rateOfFire); // 1초 - 0.1*발사 횟수만큼 대기
            for (int i = 0; i < shots; i++)
            {
                GameObject bulletInstance = Instantiate(bullet, spawner);
                bulletInstance.transform.parent = null;
                yield return new WaitForSeconds(rateOfFire); // 0.1초 대기
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _target = null;
            StopShoot();
        }
    }

    private void StopShoot()
    {
        StopAllCoroutines();
        _startedShoot = false;
    }
}