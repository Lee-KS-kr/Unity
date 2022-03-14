using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // ������ ���� ����
    public GameObject[] enemyPrefabs = null;

    // �뷮���� �����س��� �޸� Ǯ
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

    // ���� ���� �ѹ��� ȣ��
    private void Awake()
    {
        if (instance == null) // ���� ó�� ������� �ν��Ͻ���.
        {
            instance = this;
            instance.Initialize();
            DontDestroyOnLoad(this.gameObject); // �ٸ� ���� �ε�Ǵ��� �������� �ʴ´�.
        }
        else
        {
            // �̹� �ν��Ͻ��� ��������� �ִ�.
            if (instance != this) // �̹� ������� �ν��Ͻ��� ���� �ٸ���.
            {
                Destroy(this.gameObject); // ���� �״´�.
            }
        }
    }


    // �ʱ�ȭ
    public void Initialize()
    {
        // pool ������ ä���.
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
    
    // pool�� ������ �ִ� ������Ʈ���� �� ���� ������Ʈ�� �䱸�Ǿ��� �� ó���ϴ� �Լ�
    // �ʱ�ȭ�� ���� ���
    private void PoolExpand(int poolIndex, int poolSize, bool init=false)
    {
        if (-1 < poolIndex && poolIndex < enemyPrefabs.Length)
        {
            pools[poolIndex] = new Queue<GameObject>(poolSize); // ����ִ� Ǯ ����. �ʱ�ũ�� ����

            int makeCount = queueSize[poolIndex]; // ���� Instantiate�� ���� ����
            if (!init) // Initialize���� ȣ������ ��찡 �ƴϸ�
            {
                makeCount >>= 1; // Queueũ���� ���ݸ�ŭ�� ����(�̹� ������� �ִ� ������Ʈ ��ŭ�� ������ �ʿ䰡 ����)
            }

            for (int i = 0; i < makeCount; i++) // ������Ʈ �ϳ��� ����
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

    // pool���� ������Ʈ�� �ϳ� �������� �Լ�
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

    // �Ķ���ͷ� ���� enemy�� pool�� �����ִ� �Լ�
    public void ReturnEnemy(GameObject enemy)
    {
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemy.SetActive(false);
        Queue<GameObject> targetPool = pools[(int)enemyScript.Type];
        targetPool.Enqueue(enemy);
    }


    // �ϳ��� �޴� ��

    // 3�� 8�� ����
    // 1��. ���̵� ����
    // ���� õ�忡 �ε����� �״´�.
    // 2��. ���̵� ��~�߻�
    // �޸� Ǯ �ʱ�ȭ �Լ��� �����.
    // 3��. ���̵� �߻�~����
    // Pool�� ������ �ִ� ������Ʈ���� �� ���� ������Ʈ�� �䱸�Ǿ��� �� ó���ϴ� �Լ��� �����.

    // 3�� 10�� ����
    // Pool�� ������ �ִ� ������Ʈ���� �� ���� ������Ʈ�� �䱸�Ǿ��� �� ó���ϴ� �Լ��� �����.

    // ���׸� : �Ϲ�ȭ
    // Ÿ���� ������ ���� �� �ִ� ���.
}
