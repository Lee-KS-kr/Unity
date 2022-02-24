using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // ����
    // ã�ƺ��� �� �͵�
    // �Լ��� ����Ͽ� ���� ȭ�� ������ ������ ������ ���̷� �� ���� �����Ǵ� �ڵ带 �ۼ��Ͻÿ�
    //public GameObject enemyPrefab; // �� ���� �����ϰ� �����ϵ���
    public GameObject[] enemyPrefabs = null; // Enemy prefab �� ���� �ֱ� ���� �迭�� �ۼ�
    private Vector2 spawnPosition; // spawnPosition�� ��ġ y�� �������� ��� ���� Vector2
    private int enemyIndex; // Enemy ������ �������� �ϱ� ���� ����
    private bool isGame; // Spawn �Լ� ����� ���� ���Ǻ���
    private float curTime = 0.0f; // ��ȯ�Ǵ� �ð� ������ �ֱ� ���� ���� 1
    private float nextSpawnTime = 3.0f; // ������ ��ȯ �� �ð� ����

    private void Start()
    {
        isGame = true;
    }

    private void Update()
    {
        curTime += Time.deltaTime;
        if (curTime <= nextSpawnTime) return;
        Spawn();
    }
        // Enemy ������ ���� �� �ϱ� ���� �ݺ��� ���
        //for (int repeatNum = 0; repeatNum < 10; repeatNum++)
        //{
        //    // Enemy prefab�� ������ ��� ����ϱ� ���� ���߹ݺ��� ���
        //    for (int index = 0; index < enemyPrefabs.Length; index++)
        //    {
        //        // y��ǥ�� ��ġ�� �������� ����
        //        float yRange = Random.Range(-1.5f, 1.2f);
        //        // �������� ���� y��ǥ�� spawner�� x��ǥ�� �������� ���� y��ǥ�� ����
        //        spawnPosition = new Vector2(transform.position.x, yRange);
        //        // enemy ������Ű�� �ڵ�. �������� ������ ������ ��ǥ, rotation�� �⺻����
        //        Instantiate(enemyPrefabs[index], spawnPosition, Quaternion.identity);
        //    }
        //    // ���� ������ �ð� ������ �ΰ� �;..... �ڵ带 ��� �ϴµ�...
        //    // Invoke�� ���������...�ʴµ�......
        //}
        //Instantiate : �������� ���� ������Ʈ�� ���� �����ϴ� �Լ�
        //Destroy : ���� ������Ʈ�� �����ϴ� �Լ�
        //Random.Range() : ������ ���ڸ� �����ִ� �Լ�

    private void Spawn()
    {
        if (!isGame) return;
        int howMany = Random.Range(2, 6);
        for (int index = 0; index < howMany; index++)
        {
            RandomEnemy();
            float yRange = Random.Range(-1.2f, 1.2f);
            spawnPosition = new Vector2(transform.position.x, yRange);
            Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity);
        }
        curTime = 0.0f;
    }

    private int RandomEnemy()
    {
        enemyIndex = Random.Range(0, 3);
        return enemyIndex;
    }

        // Invoke("Spawn", 3); // ��� ȣ��, ���߿� trigger set�� �Ǹ� �ڿ������� ����� ��
        // Update���� �ð��� �����ؼ� �ð��� ������ ȣ��ǵ���
}
