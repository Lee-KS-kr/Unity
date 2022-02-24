using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 과제
    // 찾아봐야 할 것들
    // 함수를 사용하여 게임 화면 오른쪽 끝에서 랜덤한 높이로 적 새가 생성되는 코드를 작성하시오
    //public GameObject enemyPrefab; // 한 개만 랜덤하게 생성하도록
    public GameObject[] enemyPrefabs = null; // Enemy prefab 세 개를 넣기 위해 배열로 작성
    private Vector2 spawnPosition; // spawnPosition의 위치 y를 랜덤으로 잡기 위해 Vector2
    private int enemyIndex; // Enemy 생성을 랜덤으로 하기 위한 변수
    private bool isGame; // Spawn 함수 사용을 위한 조건변수
    private float curTime = 0.0f; // 소환되는 시간 간격을 주기 위한 변수 1
    private float nextSpawnTime = 3.0f; // 다음에 소환 될 시간 간격

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
        // Enemy 생성을 여러 번 하기 위해 반복문 사용
        //for (int repeatNum = 0; repeatNum < 10; repeatNum++)
        //{
        //    // Enemy prefab의 종류를 모두 사용하기 위해 이중반복문 사용
        //    for (int index = 0; index < enemyPrefabs.Length; index++)
        //    {
        //        // y좌표의 위치를 랜덤으로 뽑음
        //        float yRange = Random.Range(-1.5f, 1.2f);
        //        // 랜덤으로 뽑은 y좌표를 spawner의 x좌표와 랜덤으로 뽑은 y좌표에 넣음
        //        spawnPosition = new Vector2(transform.position.x, yRange);
        //        // enemy 생성시키는 코드. 포지션은 위에서 지정한 좌표, rotation은 기본으로
        //        Instantiate(enemyPrefabs[index], spawnPosition, Quaternion.identity);
        //    }
        //    // 이전 생성과 시간 간격을 두고 싶어서..... 코드를 써야 하는데...
        //    // Invoke는 쓰고싶지가...않는데......
        //}
        //Instantiate : 프리팹을 게임 오브젝트로 동적 생성하는 함수
        //Destroy : 게임 오브젝트를 삭제하는 함수
        //Random.Range() : 랜덤한 숫자를 돌려주는 함수

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

        // Invoke("Spawn", 3); // 재귀 호출, 나중에 trigger set이 되면 자연스럽게 종료될 것
        // Update에서 시간을 지정해서 시간이 지나면 호출되도록
}
