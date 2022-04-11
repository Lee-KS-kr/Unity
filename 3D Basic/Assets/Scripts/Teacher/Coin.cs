using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameObject _child = null;
    public float rotateSpeed = 360;

    private void Awake()
    {
        _child = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        _child.transform.Rotate(-Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 점수 증가
            GameManager.Inst.CoinCount++;
            Debug.Log($"코인 갯수 : {GameManager.Inst.CoinCount}");
            Destroy(gameObject);
        }
    }
}
