using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTest : MonoBehaviour
{
    public GameObject[] enemyPrefabs = null;
    public float spawnStartDelay = 0.3f;
    public float spawnInterval = 1.0f;

    private const int MAX_SPACE_COUNT = 6;
    private const float SPACE_HEIGHT = 0.4f;
    private const float LIFETIME = 5;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnStartDelay);

        // 그 후 반복해서 생성 시작
        while (true) // 무한 루프
        {
            bool result = false;
            bool[] flags = new bool[MAX_SPACE_COUNT];
            BitArray bitFlags = new BitArray(flags);

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
        // 어떤 종류의 적을 생성할지 결정
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = GameObject.Instantiate(enemyPrefabs[enemyIndex], this.transform);
        int spaceIndex = Random.Range(0, MAX_SPACE_COUNT);
        enemy.transform.Translate(Vector2.down * index * SPACE_HEIGHT);
        Destroy(enemy, LIFETIME);

    }

    //2월 28일 과제
    // 코드가 이해되지 않은 사람 -> 주석보고 코드 이해해서 오기
    // 코드가 이해된 사람 -> bit 연산을 이용해 flags를 세팅하고 사용하도록 코드를 작성하기
}
