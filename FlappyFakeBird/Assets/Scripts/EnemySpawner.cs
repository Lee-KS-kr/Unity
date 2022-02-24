using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 과제
    // 찾아봐야 할 것들
    // 함수를 사용하여 게임 화면 오른쪽 끝에서 랜덤한 높이로 적 새가 생성되는 코드를 작성하시오
    //Instantiate : 프리팹을 게임 오브젝트로 동적 생성하는 함수
    //Destroy : 게임 오브젝트를 삭제하는 함수
    //Random.Range() : 랜덤한 숫자를 돌려주는 함수

    #region Variables
    //public GameObject enemyPrefab; // 한 개만 랜덤하게 생성하도록
    public GameObject[] enemyPrefabs = null; // Enemy prefab 세 개를 넣기 위해 배열로 작성
    private Vector2 spawnPosition; // spawnPosition의 위치 y를 랜덤으로 잡기 위해 Vector2
    private int enemyIndex; // Enemy 생성을 랜덤으로 하기 위한 변수
    private bool isGame; // Spawn 함수 사용을 위한 조건변수
    private float startDelay = 1.0f; // 소환되는 시간 간격을 주기 위한 변수 1
    private float spawnInterval = 2.5f; // 다음에 소환 될 시간 간격
    #endregion

    private void Start()
    {
        isGame = true; // 나중에 게임오버 조건 생성시 false로 변환하여 Spawn함수 종료
        InvokeRepeating("Spawn", startDelay, spawnInterval);
        // 시작시 1초 후에 첫 생성, 이후 2.5초 간격으로 enemy 생성
    }

    //private void Update()
    //{
    //    curTime += Time.deltaTime;
    //    if (curTime <= nextSpawnTime) return;
    //    Spawn();
    //}
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
        //    // 이전 생성과 시간 간격을 두는 함수를 생각해보기
        //    // 생각 완료 -> Start에서 돌릴 것이므로 이 부분 주석처리
        //}

    private void Spawn()
    {
        if (!isGame) return; // 게임 진행중이 아닐 시 return하여 enemy 생성 x
        int howMany = Random.Range(2, 6); // 생성할 enemy 개체수 2~5 사이 랜덤지정
        for (int index = 0; index < howMany; index++)
        {
            RandomEnemy(); // enemy 랜덤생성
            float yRange = Random.Range(0,5)*0.5f; // y축의 위치 랜덤 지정
            spawnPosition = new Vector2(transform.position.x, transform.position.y - yRange); 
            // 스폰 위치 지정
            Instantiate(enemyPrefabs[enemyIndex], spawnPosition, Quaternion.identity); // 객체 생성
        }
    }

    // enemy의 종류도 랜덤하게 나오도록 하기 위한 함수
    private int RandomEnemy()
    {
        enemyIndex = Random.Range(0, 3);
        return enemyIndex;
    }

}
