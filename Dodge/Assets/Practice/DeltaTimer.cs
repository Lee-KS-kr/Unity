using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaTimer : MonoBehaviour
{
    float a;
    float b;

    float CounterA()
    {
        //�����Ӵ� �ϳ��� ���ϴ� ���
        return a += 1f;
    }

    float CounterB()
    {
        //�ʴ� �ϳ��� ���ϴ� ���
        return b += 1f * Time.deltaTime;
    }

    void Update()
    {
        Debug.Log($"{(int)CounterA()}, {(int)CounterB()}");
    }
}
