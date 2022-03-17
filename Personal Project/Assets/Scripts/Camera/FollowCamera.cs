using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 limitX;
    [SerializeField] private Vector3 limitZ;
    [SerializeField] private float lerp;
    private Vector3 targetPos;

    private void LateUpdate()
    {
        float posX = Mathf.Clamp(target.position.x + offset.x, limitX.x, limitX.y);
        float posY = target.position.y + offset.y;
        float posZ = Mathf.Clamp(target.position.z + offset.z, limitZ.x, limitZ.y);

        targetPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, lerp * Time.deltaTime);
    }
}