using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Old : MonoBehaviour
{
    public GameObject bullet; // 발사할 총알 오브젝트
    public Transform spawner; // 총알이 발사될 위치
    public Transform pillar;
    public Transform lookAtTarget; // 플레이어

    public float interval = 1.0f; // 총알을 발사하는 간격(방아쇠를 당기는 간격)
    public float rateOfFire = 0.1f; // 연사 간격
    public int shots = 5; // 한번 발사할 때 몇연사를 할 것인가

    public float halfAngle = 20.0f;
    public float rotateSpeed = 90;
    public float rotateDirection = 1;
    private float _targetAngle = 0;
    public float lookAtRange = 5; // 플레이어를 인식할 범위
    private bool _isLookingAt = false; // 플레이어를 쳐다보는 동안 회전을 끄기 위해 bool타입 변수 추가

    private IEnumerator _shotSave; // 코루틴용 IEnumerator 저장

    // 첫번째 update가 실행되기 직전
    private void Start()
    {
        _shotSave = Shot(); // IEnumerator 저장
        //StartCoroutine(_shotSave); // 저장한 IEnumerator로 코루틴 실행
    }

    private void Update()
    {
        RotateHead(); // 터렛의 회전을 메소드로 변경
        //CalculateDistance(); // 유저와의 거리 계산용 메소드. 
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

    // 터렛 헤드 회전
    private void RotateHead()
    {
        if (pillar) // 타겟을 바라보는 동안에는 회전하지 않도록 조건 추가 && !_isLookingAt
        {
            _targetAngle += rotateDirection * rotateSpeed * Time.deltaTime;
        
            if (Mathf.Abs(_targetAngle) > halfAngle)
            {
                rotateDirection *= -1.0f;
            }
        
            pillar.transform.rotation = Quaternion.Euler(0, _targetAngle, 0);
        }
    }

    // 타겟과의 거리를 계산하는 메소드. 인식범위 이내로 들어오면 LookAtTarget 메소드를 실행
    private void CalculateDistance()
    {
        float distance = Vector3.Distance(lookAtTarget.position, transform.position);
        if (distance <= lookAtRange)
        {
            _isLookingAt = true;
            LookAtTarget();
        }
        else
        {
            _isLookingAt = !_isLookingAt;
        }
    }
    
    // 유저의 위치를 추적하는 메소드
    private void LookAtTarget()
    {
        // 목표 위치
        Vector3 to = new Vector3(lookAtTarget.position.x, 0, lookAtTarget.position.z);
        // 내 위치
        Vector3 from = new Vector3(transform.position.x, 0, transform.position.z);

        // 바로 돌기
        pillar.transform.rotation = Quaternion.LookRotation(to - from);
    }
    
    // Scene view에서 범위를 알기 쉽도록 기즈모 그리기
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookAtRange);
    }
}