using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    // 1. 앞으로 나가기
    // 2. 충돌 감지 + 대상 해치우기

    public float bulletSpeed = 5.0f; // 총알의 이동 속도

    private Rigidbody _rigidbody; // 움직이는 물체라 rigidbody 추가

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>(); // rigidbody 캐싱
    }

    private void OnEnable()
    {
        //_rigidbody.velocity = Vector3.forward * bulletSpeed; // 물체의 이동 방향과 속도 설정
        _rigidbody.velocity = transform.forward * bulletSpeed; // 물체의 이동 방향과 속도 설정
        Destroy(gameObject, 3); // 3초 후 파괴
    }

    private void OnDisable()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    // 다른 collider와 충돌했을때 실행되는 함수
    private void OnCollisionEnter(Collision collision)
    {
        // 태그 : 특정한 무언가를 확인할 때
        // 인터페이스 : 특정한 카테고리에 속하는 것들을 처리할 때

        // Debug.Log(collision.gameObject.name);
        // 죽일 수 있는 대상이면 죽인다.
        IDead target = collision.gameObject.GetComponent<IDead>();

        if (target != null)
        {
            target.OnDead(); // IDead가 붙어있는(죽일 수 있는) 대상이라 죽인다
        }

        // 총알이 뭔가와 부딪혔을 때 이얼아냐 할 일들
        if (!_rigidbody.useGravity) // 우리가 초기에 세팅한 그대로라면
        {
            _rigidbody.useGravity = true; // 중력을 켜고
            Vector3 reflect = Vector3.Reflect(transform.forward, collision.GetContact(0).normal); // 반사된 벡터 계싼
            _rigidbody.AddForce(collision.GetContact(0).normal * 2.0f, ForceMode.Impulse); // 부딪혀서 튕기는 느낌을 추가
            Vector3 randomDir = new Vector3(Random.value, Random.value, Random.value); // 랜덤 방향 지정
            _rigidbody.AddTorque(randomDir * 5.0f, ForceMode.Impulse); // 바닥에 떨어져서 구르는 느낌 추가
        }
    }
}
