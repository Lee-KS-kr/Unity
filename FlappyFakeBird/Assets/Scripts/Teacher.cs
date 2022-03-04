using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : MonoBehaviour
{
    public GameObject[] enemyPrefabs = null;
    private GameManager gameManager;
    public float spawnStartDelay = 0.3f;
    public float spawnInterval = 1.0f;
    private const int MAX_SPACE_COUNT = 6;
    private const float SPACE_HEIGHT = 0.4f;
    private const float LIFETIME = 5;
    private int enemyCount = 0; // 에너미 마리수를 세기 위한 변수
    private Queue queue = new Queue(); // 마리수에 따라 점수를 상승시키기 위함

    // 조건 새는 한번에 최소 1칸에서 최대 4칸까지 생성이 가능
    // 조건 새는 생성될 때 최소 두 개의 빈칸이 연속적으로 있어야 한다.
    // 조건을 결과는 같지만 내용이 다르게 한다면
        // 첫번째 조건을 랜덤으로 on, off 결정
        // 0~4번을 랜덤으로 선택하고 선택된 칸과 그 다음 칸을 off설정
        // on으로 설정된 부분만 새를 생성

    // 최초의 Update 실행 직전에 한번만 호출
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
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
            // bool[] flags = GetFlagsBoolType();
            int flags = GetFlags(); // 각 비트를 확인해서 1일 때만 새를 생성
            int singleFlag = 1;
            enemyCount = 0; // 점수가 계속 누적되지 않게 하기 위해 0으로
            for (int i = 0; i < MAX_SPACE_COUNT; i++)
            {
                // if (flags[i] == true) // GetFlagsBoolType용 조건문
                // flags와 singleFlag를 &해서 0이 아니면 singleFlag에 설정된 비트 위치에 1이 되어있다는 것
                if ((flags & singleFlag) != 0)
                {
                    EnemyGenerate(i);
                    enemyCount++; // 한마리 생성시마다 에너미 카운트를 추가
                }
                singleFlag <<= 1; //singleFlag의 비트를 한번 컴사할 떄 마다 왼쪽으로 한칸씩 옮김
            }
            queue.Enqueue(enemyCount); // 확인한 에너미 카운트를 큐에 추가
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private int GetFlags()
    {
        int flags = 0; // 리턴용 변수. 최종적으로 계산한 플래그 값이 들어갈 변수
        // 6비트만 남겨 놓을 예정. 1로 설정된 칸에서는 적이 생성되고, 0인 곳은 아무것도 생성되지 않는다.
        do
        {
            int random = (int)(Random.value * 100.0f); // 화이트보드 1번
            random &= ((1 << MAX_SPACE_COUNT) - 1);
            int mask = 0b_0011;
            mask = mask << Random.Range(0, MAX_SPACE_COUNT - 1); // mask를 랜덤하게 쉬프트
            mask = ~mask; // not연산을 통해 bit값 뒤집기
            flags = random & mask; // 최종적으로 random값에 mask값을 &시켜서 두칸 비우기
        } while (flags == 0); // 모든 칸이 비는 것을 방지하기 위해 설정

        return flags;
    }

    private static bool[] GetFlagsBoolType()
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

        return flags;
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

    public int GetQueue() // 게임매니저에서 적이 라인캐스트에 감지될때 가져오기 위한 매서드
    {
        return (int)queue.Dequeue();
    }

    //2월 28일 과제
    // 코드가 이해되지 않은 사람 -> 주석보고 코드 이해해서 오기
    // 코드가 이해된 사람 -> bit 연산을 이용해 flags를 세팅하고 사용하도록 코드를 작성하기

}