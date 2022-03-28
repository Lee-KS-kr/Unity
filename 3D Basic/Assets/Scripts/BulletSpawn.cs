using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnerPosition;
    [SerializeField] private float _spawnRate = 1.0f;
    [SerializeField] private float _spawnInterval = 5.0f;

    private void OnEnable()
    {
        StartCoroutine(SpawnBullet());
    }

    private IEnumerator SpawnBullet()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(bulletPrefab,spawnerPosition);
                yield return new WaitForSeconds(_spawnRate);
            }

            yield return new WaitForSeconds(_spawnInterval);
        }
    }
}