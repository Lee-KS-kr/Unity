using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // ����
    // ã�ƺ��� �� �͵�
    // �Լ��� ����Ͽ� ���� ȭ�� ������ ������ ������ ���̷� �� ���� �����Ǵ� �ڵ带 �ۼ��Ͻÿ�
    //Instantiate : �������� ���� ������Ʈ�� ���� �����ϴ� �Լ�
    //Destroy : ���� ������Ʈ�� �����ϴ� �Լ�
    //Random.Range() : ������ ���ڸ� �����ִ� �Լ�

    #region Variables
    //public GameObject enemyPrefab; // �� ���� �����ϰ� �����ϵ���
    public GameObject[] enemyPrefabs = null; // Enemy prefab �� ���� �ֱ� ���� �迭�� �ۼ�
    private Vector2 spawnPosition; // spawnPosition�� ��ġ y�� �������� ��� ���� Vector2
    private int enemyIndex; // Enemy ������ �������� �ϱ� ���� ����
    private bool isGame; // Spawn �Լ� ����� ���� ���Ǻ���
    private float startDelay = 1.0f; // ��ȯ�Ǵ� �ð� ������ �ֱ� ���� ���� 1
    private float spawnInterval = 2.5f; // ������ ��ȯ �� �ð� ����
    #endregion

    private void Start()
    {
        isGame = true; // ���߿� ���ӿ��� ���� ������ false�� ��ȯ�Ͽ� Spawn�Լ� ����
        InvokeRepeating("Spawn", startDelay, spawnInterval);
        // ���۽� 1�� �Ŀ� ù ����, ���� 2.5�� �������� enemy ����
    }

    //private void Update()
    //{
    //    curTime += Time.deltaTime;
    //    if (curTime <= nextSpawnTime) return;
    //    Spawn();
    //}
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
        //    // ���� ������ �ð� ������ �δ� �Լ��� �����غ���
        //    // ���� �Ϸ� -> Start���� ���� ���̹Ƿ� �� �κ� �ּ�ó��
        //}

    private void Spawn()
    {
        if (!isGame) return; // ���� �������� �ƴ� �� return�Ͽ� enemy ���� x
        int howMany = Random.Range(2, 6); // ������ enemy ��ü�� 2~5 ���� ��������
        for (int index = 0; index < howMany; index++)
        {
            RandomEnemy(); // enemy ��������
            float yRange = Random.Range(0,5)*0.5f; // y���� ��ġ ���� ����
            spawnPosition = new Vector2(transform.position.x, transform.position.y - yRange); 
            // ���� ��ġ ����
            Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity); // ��ü ����
        }
    }

    // enemy�� ������ �����ϰ� �������� �ϱ� ���� �Լ�
    private int RandomEnemy()
    {
        enemyIndex = Random.Range(0, 3);
        return enemyIndex;
    }

}
