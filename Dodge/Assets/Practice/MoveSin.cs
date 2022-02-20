using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSin : MonoBehaviour
{
    private float theta = 0f;
    public float speed_y = 1f;
    public float height = 1f;

    void Update()
    {
        Vector3 pos = transform.position;
        theta += speed_y * Time.deltaTime;
        pos.x = Mathf.Sin(theta) * height;
        pos.y = Mathf.Cos(theta) * height;
        this.transform.position = pos;
    }
}
