using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 1. 앞으로 나가기
    // 2. 충돌 감지 + 대상 해치우기

    public float bulletSpeed = 5.0f;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _rigidbody.velocity = Vector3.forward * bulletSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 태그 : 특정한 무언가를 확인할 때
        // 인터페이스 : 특정한 카테고리에 속하는 것들을 처리할 때

        Debug.Log(collision.gameObject.name);
        IDead target = collision.gameObject.GetComponent<IDead>();

        if (target != null)
        {
            target.OnDead();
        }

        if (!_rigidbody.useGravity)
        {
            _rigidbody.useGravity = true;
            _rigidbody.AddForce(collision.GetContact(0).normal * 2.0f, ForceMode.Impulse);
            _rigidbody.AddTorque(Vector3.one * 5.0f, ForceMode.Impulse);
            Destroy(gameObject, 3.0f);
        }
    }
}
