using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    // �ǽ�
    // ����� ���� ������Ʈ �����
    // �̸� : Airplane
    // ��� : �⺻ ���� or ���κ���
    // ��ũ��Ʈ �ϼ�

    //public GameObject[] propellerPrefab;
    //private float propellerSpinSpeed = 1800.0f;
    private Transform _propTransform = null;
    public Transform[] spotPosition;
    private Rigidbody _airplaneRigidbody;

    public float propSpeed = 1500;
    public bool propeller = false; // true�� �����緯�� ���ư��� false�� �ȵ��ư���.
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