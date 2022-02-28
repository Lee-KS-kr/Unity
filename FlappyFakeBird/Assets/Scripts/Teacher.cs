using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : MonoBehaviour
{
    public GameObject[] enemyPrefabs = null;
    public float spawnStartDelay = 0.3f;
    public float spawnInterval = 1.0f;

    private const int MAX_SPACE_COUNT = 6;
    private const float SPACE_HEIGHT = 0.4f;
    private const float LIFETIME = 5;

    // ���� ���� �ѹ��� �ּ� 1ĭ���� �ִ� 4ĭ���� ������ ����
    // ���� ���� ������ �� �ּ� �� ���� ��ĭ�� ���������� �־�� �Ѵ�.
    // ������ ����� ������ ������ �ٸ��� �Ѵٸ�
        // ù��° ������ �������� on, off ����
        // 0~4���� �������� �����ϰ� ���õ� ĭ�� �� ���� ĭ�� off����
        // on���� ������ �κи� ���� ����

    // ������ Update ���� ������ �ѹ��� ȣ��
    private void Start()
    {
        // Spawn�Լ��� �ڷ�ƾ���� ����
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        // �������� �� spawnStartDelay��ŭ ���
        yield return new WaitForSeconds(spawnStartDelay);
        
        // �� �� �ݺ��ؼ� ���� ����
        while (true) // ���� ����
        {
            bool result = false;
            bool[] flags = new bool[MAX_SPACE_COUNT];
            while (result == false)
            {
                for (int i = 0; i < MAX_SPACE_COUNT; i++)
                {
                    if (Random.Range(0, 2) == 1)
                        flags[i] = true;
                }
                int index = Random.Range(0, MAX_SPACE_COUNT - 1);
                flags[index] = false;
                flags[index + 1] = false;

                for (int i = 0; i < MAX_SPACE_COUNT; i++)
                {
                    if (flags[i] == true)
                    {
                        result = true;
                        break;
                    }
                }
            }

            for (int i = 0; i < MAX_SPACE_COUNT; i++)
            {
                if (flags[i] == true)
                    EnemyGenerate(i);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void EnemyGenerate(int index)
    {
        // � ������ ���� �������� ����
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = GameObject.Instantiate(enemyPrefabs[enemyIndex], this.transform);
        int spaceIndex = Random.Range(0, MAX_SPACE_COUNT);
        enemy.transform.Translate(Vector2.down * index * SPACE_HEIGHT);
        Destroy(enemy, LIFETIME);

    }

    //2�� 28�� ����
    // �ڵ尡 ���ص��� ���� ��� -> �ּ����� �ڵ� �����ؼ� ����
    // �ڵ尡 ���ص� ��� -> bit ������ �̿��� flags�� �����ϰ� ����ϵ��� �ڵ带 �ۼ��ϱ�

}