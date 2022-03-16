using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private const int DEFAULT_POOL_SIZE = 16;
    //private const int DEFAULT_POOL_SIZE = 2;
    private const int RANDOM_INDEX = -1;

    private int[] queueSize = null;  //�ش� ť�� ���� ũ�� + ���� �����Ǵ� �� �̸��� ���� �ε��� ��ȣ�ε� �ο�

    // �뷮���� ������ ���� �޸� Ǯ
    private Queue<GameObject>[] pools = null;   //�� �������� ���� Queue�� ������ �迭

    // ������ ���� ����
    public GameObject[] enemyPrefabs = null;

    private static ObjectPool instance = null;
    public static ObjectPool Inst
    {
        get
        {
            return instance;
        }
    }

    //���� ���� �ѹ��� ȣ��
    private void Awake()
    {
        if (instance == null)   // ���� ó�� ������� �ν��Ͻ���.
        {
            instance = this;
            instance.Initialize();
            DontDestroyOnLoad(this.gameObject); // �ٸ� ���� �ε�Ǵ��� �������� �ʴ´�.
        }
        else
        {
            // �̹� �ν��Ͻ��� ��������� �ִ�.
            if (instance != this)    // �̹� ������� ���� ���� �ٸ���.
            {
                Destroy(this.gameObject);   //���� �״´�.
            }
        }
    }


    // �ʱ�ȭ(pool������ ä���.)
    // ����) (�� ������ �ִ� 16������ ����) * (3����) = 48��. => Instantiate�� 48�� �Ѵ�.
    public void Initialize()
    {
        pools = new Queue<GameObject>[enemyPrefabs.Length]; // �� ������ ����ŭ �迭 ũ�� Ȯ��
        queueSize = new int[enemyPrefabs.Length];  //�� Ǯ�� ũ�Ⱚ �ʱ�ȭ

        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            PoolExpand(i, DEFAULT_POOL_SIZE, true); //�� �������� Ǯ �����ϰ� ä���
        }
    }

    // Pool�� ó�� �����ϰų� ũ�⸦ Ȯ���ϰ� ������(pool�� ������Ʈ�� �ٴڳ��� ��) ȣ���ϴ� �Լ�
    private void PoolExpand(EnemyType type, int poolSize, bool init = false)
    {
        int index = -1;
        //switch (type)
        //{
        //    case EnemyType.NORMAL:
        //        index = 0;
        //        break;
        //    case EnemyType.BLUE:
        //        index = 1;
        //        break;
        //    case EnemyType.RED:
        //        index = 2;
        //        break;
        //    case EnemyType.INVALID:
        //        break;
        //}
        index = (int)type;
        PoolExpand(index, poolSize, init);
    }

    private void PoolExpand(int poolIndex, int poolSize, bool init = false)
    {
        //poolIndex�� ������ �������� Ȯ��
        if (-1 < poolIndex && poolIndex < enemyPrefabs.Length)
        {
            pools[poolIndex] = new Queue<GameObject>(poolSize); // ����ִ� Ǯ ����. �ʱ� ũ�� ����

            int makeSize = poolSize;    // ���� Instanticate�� ���� ����
            if (!init)                 // Initialize �Լ����� ȣ������ ��찡 �ƴϸ�
            {                           // Queue ũ���� ���ݸ�ŭ�� ����(�̹� ������� �ִ� ������Ʈ��ŭ�� ������ �ʿ䰡 �����ϱ�)
                makeSize >>= 1;         //makeCount = makeCount / 2;  makeCount = makeCount >> 1;
            }

            for (int i = 0; i < makeSize; i++)    // ������Ʈ �ϳ��� ����
            {
                GameObject obj = GameObject.Instantiate(enemyPrefabs[poolIndex], this.transform);   //������Ʈ ����� EnemyPool�� �ڽ����� ����
                obj.name = $"{enemyPrefabs[poolIndex].name}_{queueSize[poolIndex]}";    //�̸� ����
                obj.SetActive(false);           //������Ʈ ��Ȱ��ȭ
                Enemy enemy = obj.GetComponent<Enemy>();
                switch (poolIndex)
                {
                    case 0:
                        enemy.Type = EnemyType.NORMAL;
                        break;
                    case 1:
                        enemy.Type = EnemyType.BLUE;
                        break;
                    case 2:
                        enemy.Type = EnemyType.RED;
                        break;
                    default:
                        break;
                }

                pools[poolIndex].Enqueue(obj);  //ť�� ������Ʈ ����
                queueSize[poolIndex]++;         //queueSize ����
            }
        }
    }

    // Pool���� ������Ʈ�� �ϳ� �������� �Լ�(index�� ������ ����, �⺻�����δ� �������� ����)
    public GameObject GetEnemy(int index = RANDOM_INDEX)
    {
        GameObject result = null;   //���Ͽ����� ���� ����
        int target = index;         //target�� ���� ������ ���� ����

        //�������� �����Ǵ� ���
        //index�� RANDOM_INDEX(-1)�϶�.
        //index�� ���� ������ �������� ū ���� ������ ��(ex:enemyPrefabs.Length�� 3�ε� index�� 5�� �Է¹޾Ҵ�.)
        //index�� RANDOM_INDEX(-1) ���� ���� �� 
        if (index == RANDOM_INDEX || index >= enemyPrefabs.Length || index < RANDOM_INDEX)
        {
            target = Random.Range(0, enemyPrefabs.Length);  // �������� ������ ���� ����
        }

        // ť�� ��� ������ ������Ʈ�� �ִ��� Ȯ��(ť�� ����ִ��� Ȯ��)
        if (pools[target].Count > 0)
        {
            // ť�� ������Ʈ�� �ִ�.
            result = pools[target].Dequeue();   //Dequeue�� ���� ������Ʈ �ϳ� ����
            result.SetActive(true);             // ������Ʈ Ȱ��ȭ             
        }
        else
        {
            // ť�� ������Ʈ�� ����.
            // ������ Ȯ�� �۾� ����
            PoolExpand(target, queueSize[target] << 1);   // ť�� �ι�� Ŀ���� ������Ʈ�� �߰��Ǵ� �Լ�
            result = GetEnemy(target);
        }

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
