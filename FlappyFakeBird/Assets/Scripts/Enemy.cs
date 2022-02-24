using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // ������ ����
    // �� ���� ������Ʈ�� ������ ���۵Ǹ� -x�� �������� ������ �ӵ��� �̵��Ѵ�.
    // �̵��ϴ� �ӵ��� moveSpeed��� ������ ����Ǿ� �ִ�.
    public float moveSpeed = 1.5f;

    private void Start()
    {
        Destroy(gameObject, 5.5f);
    }

    private void Update()
    {
        // enemy�� x���͸� ���̳ʽ� �������� �̵�...
        // ���� ��ġ���� �Ķ���ͷ� �Է¹��� ��ŭ ���ݾ� �����δ�.
        transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
    }
}
