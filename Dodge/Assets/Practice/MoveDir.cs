using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDir : MonoBehaviour
{
    public float height = 1f;
    private float dir = 1f;
    public float step = 1f;

    private void Update()
    {
        if(transform.position.y >= height)
        {
            dir = -1f;
        }
        else if(transform.position.y <= 0)
        {
            dir = 1f;
        }

        Vector3 _newPosition = transform.position;
        _newPosition.y = _newPosition.y + dir * Time.deltaTime * step;
        transform.position = _newPosition;
    }
}
