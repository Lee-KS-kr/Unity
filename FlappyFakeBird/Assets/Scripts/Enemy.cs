using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType : byte
{
    NORMAL = 0,
    RED,
    BLUE,
    INVALID
}

public class Enemy : MonoBehaviour
{
    // ������ ����
    // �� ���� ������Ʈ�� ������ ���۵Ǹ� -x�� �������� ������ �ӵ��� �̵��Ѵ�.
    // �̵��ϴ� �ӵ��� moveSpeed��� ������ ����Ǿ� �ִ�.
    public float moveSpeed = 1.5f;
    private Rigidbody2D enemyRb;
    private EnemyType type = EnemyType.INVALID;
    public EnemyType Type
    {
        get
        {
            return type;
        }
        set
        {
            if (type == EnemyType.INVALID)
            {
                type = value; // �ѹ��� �� �� �ִ�.
            }
        }
    }

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        //Destroy(gameObject, 5.5f); // Instantiate ���� 5.5�� �Ŀ� ������
        //StartCoroutine(DisableEnemy());
    }

    private void Update()
    {
        // enemy�� x���͸� ���̳ʽ� �������� �̵�...
        // ���� ��ġ���� �Ķ���ͷ� �Է¹��� ��ŭ ���ݾ� �����δ�.
        transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
    }

    //private IEnumerator DisableEnemy()
    //{
    //    yield return new WaitForSeconds(returnTime);
    //    ObjectPool.ReturnObject(this);
    //}
}
