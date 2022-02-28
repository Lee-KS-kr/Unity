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

    // 조건 새는 한번에 최소 1칸에서 최대 4칸까지 생성이 가능
    // 조건 새는 생성될 때 최소 두 개의 빈칸이 연속적으로 있어야 한다.
    // 조건을 결과는 같지만 내용이 다르게 한다면
        // 첫번째 조건을 랜덤으로 on, off 결정
        // 0~4번을 랜덤으로 선택하고 선택된 칸과 그 다음 칸을 off설정
        // on으로 설정된 부분만 새를 생성

    // 최초의 Update 실행 직전에 한번만 호출
    private void Start()
    {
        // Spawn함수를 코루틴으로 실행
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        // 실행했을 때 spawnStartDelay만큼 대기
        yield return new WaitForSeconds(spawnStartDelay);
        
        // 그 후 반복해서 생성 시작
        while (true) // 무한 루프
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