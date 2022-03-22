using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    // 실습
    // 비행기 게임 오브젝트 만들기
    // 이름 : Airplane
    // 방식 : 기본 도형 or 프로빌더
    // 스크립트 완성

    //public GameObject[] propellerPrefab;
    //private float propellerSpinSpeed = 1800.0f;
    private Transform _propTransform = null;
    public Transform[] spotPosition;
    private Rigidbody _airplaneRigidbody;

    public float propSpeed = 1500;
    public bool propeller = false; // true면 프로펠러가 돌아가고 false면 안돌아간다.
    public float flightSpeed = 5.0f;

    private void Awake()
    {
        propeller = true;
        _propTransform = transform.GetChild(0).Find("Propeller");
        _airplaneRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //if (propeller)
        //{
        //    for (int i = 0; i < propellerPrefab.Length; i++)
        //    {
        //        propellerPrefab[i].transform.Rotate(-Vector3.forward * propellerSpinSpeed * Time.deltaTime, Space.Self);
        //    }
        //}

        _propTransform.Rotate(0, 0, -propSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (transform.position == new Vector3(0, 0, 0) ||
            Vector3.Distance(transform.position, spotPosition[spotPosition.Length-1].position) < 0.2f)
            transform.LookAt(spotPosition[0].position);
        
        for (int i = 0; i < spotPosition.Length - 1; i++)
        {
            if (Vector3.Distance(transform.position, spotPosition[i].position) < 0.3f)
            {
                transform.LookAt(spotPosition[++i].position);
            }
        }
        
        _airplaneRigidbody.MovePosition(_airplaneRigidbody.position +
                                        transform.forward * flightSpeed * Time.fixedDeltaTime);
    }
}