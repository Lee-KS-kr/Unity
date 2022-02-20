using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaTimer : MonoBehaviour
{
    float a;
    float b;

    float CounterA()
    {
        //프레임당 하나씩 더하는 경우
        return a += 1f;
    }

    float CounterB()
    {
        //초당 하나씩 더하는 경우
        return b += 1f * Time.deltaTime;
    }

    void Update()
    {
        Debug.Log($"{(int)CounterA()}, {(int)CounterB()}");
    }
}
