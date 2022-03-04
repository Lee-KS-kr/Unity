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
    private int enemyCount = 0; // ���ʹ� �������� ���� ���� ����
    private Queue queue = new Queue(); // �������� ���� ������ ��½�Ű�� ����

    // ���� ���� �ѹ��� �ּ� 1ĭ���� �ִ� 4ĭ���� ������ ����
    // ���� ���� ������ �� �ּ� �� ���� ��ĭ�� ���������� �־�� �Ѵ�.
    // ������ ����� ������ ������ �ٸ��� �Ѵٸ�
        // ù��° ������ �������� on, off ����
        // 0~4���� �������� �����ϰ� ���õ� ĭ�� �� ���� ĭ�� off����
        // on���� ������ �κи� ���� ����

    // ������ Update ���� ������ �ѹ��� ȣ��
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
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
            // bool[] flags = GetFlagsBoolType();
            int flags = GetFlags(); // �� ��Ʈ�� Ȯ���ؼ� 1�� ���� ���� ����
            int singleFlag = 1;
            enemyCount = 0; // ������ ��� �������� �ʰ� �ϱ� ���� 0����
            for (int i = 0; i < MAX_SPACE_COUNT; i++)
            {
                // if (flags[i] == true) // GetFlagsBoolType�� ���ǹ�
                // flags�� singleFlag�� &�ؼ� 0�� �ƴϸ� singleFlag�� ������ ��Ʈ ��ġ�� 1�� �Ǿ��ִٴ� ��
                if ((flags & singleFlag) != 0)
                {
                    EnemyGenerate(i);
                    enemyCount++; // �Ѹ��� �����ø��� ���ʹ� ī��Ʈ�� �߰�
                }
                singleFlag <<= 1; //singleFlag�� ��Ʈ�� �ѹ� �Ļ��� �� ���� �������� ��ĭ�� �ű�
            }
            queue.Enqueue(enemyCount); // Ȯ���� ���ʹ� ī��Ʈ�� ť�� �߰�
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private int GetFlags()
    {
        int flags = 0; // ���Ͽ� ����. ���������� ����� �÷��� ���� �� ����
        // 6��Ʈ�� ���� ���� ����. 1�� ������ ĭ������ ���� �����ǰ�, 0�� ���� �ƹ��͵� �������� �ʴ´�.
        do
        {
            int random = (int)(Random.value * 100.0f); // ȭ��Ʈ���� 1��
            random &= ((1 << MAX_SPACE_COUNT) - 1);
            int mask = 0b_0011;
            mask = mask << Random.Range(0, MAX_SPACE_COUNT - 1); // mask�� �����ϰ� ����Ʈ
            mask = ~mask; // not������ ���� bit�� ������
            flags = random & mask; // ���������� random���� mask���� &���Ѽ� ��ĭ ����
        } while (flags == 0); // ��� ĭ�� ��� ���� �����ϱ� ���� ����

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
        // � ������ ���� �������� ����
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemy = GameObject.Instantiate(enemyPrefabs[enemyIndex], this.transform);
        int spaceIndex = Random.Range(0, MAX_SPACE_COUNT);
        enemy.transform.Translate(Vector2.down * index * SPACE_HEIGHT);
        Destroy(enemy, LIFETIME);

    }

    public int GetQueue() // ���ӸŴ������� ���� ����ĳ��Ʈ�� �����ɶ� �������� ���� �ż���
    {
        return (int)queue.Dequeue();
    }

    //2�� 28�� ����
    // �ڵ尡 ���ص��� ���� ��� -> �ּ����� �ڵ� �����ؼ� ����
    // �ڵ尡 ���ص� ��� -> bit ������ �̿��� flags�� �����ϰ� ����ϵ��� �ڵ带 �ۼ��ϱ�

}