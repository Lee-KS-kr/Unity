using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MemoryPool : MonoBehaviour
{
    private int currentSize = 0; //큐의 현재 크기 + 새로 생성되는 적 이름에 붙일 인덱스 번호로도 싸용
    private Queue<GameObject> pool = null; // 대량으로 생성해 놓은 메모리 풀

    public GameObject objectPrefabs = null; // 생성할 오브젝트의 종류
    public int poolSize = 8;

    private static MemoryPool instance = null; // 싱글톤은 나중에 제거할 부분

    public static MemoryPool Inst
    {
        get { return instance; }
    }

    //생성 직후 한번만 호출
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
            if (instance != this) // 이미 만들어진 것이 나와 다르다.
            {
                Destroy(this.gameObject); //나는 죽는다.
            }
        }
    }


    // 초기화(처음용 pool을 만든다)
    public void Initialize()
    {
        pool = null; // 적 종류의 수만큼 배열 크기 확정
        currentSize = 0; //각 풀의 크기값 초기화

        PoolExpand(poolSize, true); //적 종류별로 풀 생성하고 채우기
    }

    private void PoolExpand(int targetSize, bool init = false)
    {
        pool = new Queue<GameObject>(targetSize); // 비어있는 풀 생성. 초기 크기 지정

        int makeSize = targetSize; // 실제 Instantiate할 갯수 설정
        if (!init) // Initialize 함수에서 호출했을 경우가 아니면
        {
            // Queue 크기의 절반만큼만 생성(이미 만들어져 있던 오브젝트만큼은 생성할 필요가 없으니까)
            makeSize >>= 1; //makeCount = makeCount / 2;  makeCount = makeCount >> 1;
        }

        for (int i = 0; i < makeSize; i++) // 오브젝트 하나씩 생성
        {
            GameObject obj =
                GameObject.Instantiate(objectPrefabs, this.transform); //오브젝트 만들고 MemoryPool의 자식으로 붙임
            obj.name = $"{objectPrefabs.name}_{currentSize}"; //이름 변경
            obj.SetActive(false); //오브젝트 비활성화

            pool.Enqueue(obj); //큐에 오브젝트 삽입
            currentSize++; //currentSize 증가
        }
    }

    // Pool에서 오브젝트를 하나 가져오는 함수
    public GameObject GetObject()
    {
        GameObject result = null; //리턴용으로 변수 생성

        // 큐에 사용 가능한 오브젝트가 있는지 확인(큐가 비어있는지 확인)
        if (pool.Count <= 0)
        {
            // 큐에 오브젝트가 없다. 없으니 확장 작업 실행
            PoolExpand(currentSize << 1); // 큐가 두배로 커지고 오브젝트도 추가되는 함수
        }
        
        // 큐에 오브젝트가 있다.
        result = pool.Dequeue(); //Dequeue를 통해 오브젝트 하나 꺼냄
        result.SetActive(true); // 오브젝트 활성화

        return result;
    }

    // 파라메터로 받은 object를 pool로 돌려주는 함수
    public void ReturnEnemy(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}