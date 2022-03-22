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
    private Transform propTransform = null;
    public float propSpeed = 1500;
    public bool propeller = false; // true�� �����緯�� ���ư��� false�� �ȵ��ư���.

    private void Awake()
    {
        propeller = true;
        propTransform = transform.Find("Propeller");
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

        propTransform.Rotate(0, 0, -propSpeed * Time.deltaTime);

    }
}
