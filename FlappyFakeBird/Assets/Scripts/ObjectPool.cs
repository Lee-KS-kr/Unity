using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // 생성할 적의 종류
    public GameObject[] enemyPrefabs = null;

    // 대량으로 생성해놓은 메모리 풀
    private Queue<GameObject>[] pools = null;
    private const int DEFAULT_POOLSIZE = 20;
    private const int RANDOM_INDEX = -1;
    private int[] queueSize = null;
    private static ObjectPool instance = null;
    public static ObjectPool Inst
    {
        get
        {
            return instance;
        }
    }

    // 생성 직후 한번만 호출
    private void Awake()
    {
        if (instance == null) // 제일 처음 만들어진 인스턴스다.
        {
            instance = this;
            instance.Initialize();
            DontDestroyOnLoad(this.gameObject); // 다른 씬이 로드되더라도 삭제되지 않는다.
        }
        else
        {
            // 이미 인스턴스가 만들어진게 있다.
            if (instance != this) // 이미 만들어진 인스턴스가 나와 다르다.
            {
                Destroy(this.gameObject); // 나는 죽는다.
            }
        }
    }


    // 초기화
    public void Initialize()
    {
        // pool 변수를 채운다.
        pools = new Queue<GameObject>[enemyPrefabs.Length];
        queueSize = new int[enemyPrefabs.Length];
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            //pools[i] = new Queue<GameObject>(DEFAULT_POOLSIZE);
            //for(int j = 0; j < DEFAULT_POOLSIZE; j++)
            //{
            //    GameObject obj = GameObject.Instantiate(enemyPrefabs[i], this.transform);
            //    obj.name = $"{enemyPrefabs[i].name}_{indexCount[i]}";
            //    obj.SetActive(false);
            //    pools[i].Enqueue(obj);
            //    indexCount[i]++;
            //}
            PoolExpand(i, DEFAULT_POOLSIZE, true);
        }
    }

    private void PoolExpand(EnemyType type, int poolSize, bool init = false)
    {
        int index = -1;
        index = (int)type;
        PoolExpand(index, poolSize, init);
    }
    
    // pool이 가지고 있는 오브젝트보다 더 많은 오브젝트가 요구되었을 때 처리하는 함수
    // 초기화할 때도 사용
    private void PoolExpand(int poolIndex, int poolSize, bool init=false)
    {
        if (-1 < poolIndex && poolIndex < enemyPrefabs.Length)
        {
            pools[poolIndex] = new Queue<GameObject>(poolSize); // 비어있는 풀 생성. 초기크기 지정

            int makeCount = queueSize[poolIndex]; // 실제 Instantiate할 개수 설정
            if (!init) // Initialize에서 호출했을 경우가 아니면
            {
                makeCount >>= 1; // Queue크기의 절반만큼만 생성(이미 만들어져 있는 오브젝트 만큼은 생성할 필요가 없음)
            }

            for (int i = 0; i < makeCount; i++) // 오브젝트 하나씩 생성
            {
                GameObject obj = GameObject.Instantiate(enemyPrefabs[poolIndex], this.transform);
                obj.name = $"{enemyPrefabs[poolIndex].name}_{queueSize[poolIndex]}";
                obj.SetActive(false);
                Enemy enemy = obj.GetComponent<Enemy>();
                switch (poolIndex)
                {
                    case 0:
                        enemy.Type = EnemyType.NORMAL;
                        break;
                    case 1:
                        enemy.Type = EnemyType.RED;
                        break;
                    case 2:
                        enemy.Type = EnemyType.BLUE;
                        break;
                    default:
                        break;
                }

                pools[poolIndex].Enqueue(obj);
                queueSize[poolIndex]++;
            }
        }
    }

    // pool에서 오브젝트를 하나 가져오는 함수
    public GameObject GetEnemy(int index=RANDOM_INDEX)
    {
        GameObject result = null;
        int target = index;
        if (index == RANDOM_INDEX || index >= enemyPrefabs.Length || index < RANDOM_INDEX)
        {
            target = Random.Range(0, enemyPrefabs.Length);
        }

        if (pools[target].Count > 0)
            result = pools[target].Dequeue();
        else
        {
            PoolExpand(target, queueSize[target] << 1, false);
            result = GetEnemy(target);
        }

        result.SetActive(true);
        return result;
    }

    // 파라메터로 받은 enemy를 pool로 돌려주는 함수
    public void ReturnEnemy(GameObject enemy)
    {
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemy.SetActive(false);
        Queue<GameObject> targetPool = pools[(int)enemyScript.Type];
        targetPool.Enqueue(enemy);
    }


    // 하나씩 받는 것

    // 3월 8일 과제
    // 1번. 난이도 최하
    // 새가 천장에 부딪히면 죽는다.
    // 2번. 난이도 중~중상
    // 메모리 풀 초기화 함수를 만든다.
    // 3번. 난이도 중상~상하
    // Pool이 가지고 있는 오브젝트보다 더 많은 오브젝트가 요구되었을 때 처리하는 함수를 만든다.

    // 3월 10일 과제
    // Pool이 가지고 있는 오브젝트보다 더 많은 오브젝트가 요구되었을 때 처리하는 함수를 만든다.

    // 제네릭 : 일반화
    // 타입을 변수로 받을 수 있는 방법.
}
