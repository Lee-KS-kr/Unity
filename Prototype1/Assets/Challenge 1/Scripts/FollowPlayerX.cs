using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerX : MonoBehaviour
{
    public GameObject plane;
    private Vector3 offset = new Vector3(31, 0, 9);

    void FixedUpdate()
    {
        transform.position = plane.transform.position + offset;
    }
}
