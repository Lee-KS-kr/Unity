// ���ӽ����̽� ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scroller Ŭ���� ����(����� Ŭ����, �Լ�, ������ � ������ �������� ���ϴ� ��)
public class Scroller : MonoBehaviour
{
    // ���� ������ ����
    public float scrollSpeed = 15.0f; // ����� �̵��ϴ� �ӵ� public���� �����ؼ� ����Ƽ �����Ϳ����� ���� �����ϰ� ����
    private GameObject[] backgrounds = null; // ��� �� ���� ������ ��� �ִ� �迭
    private GameObject frontBG = null; // ���� �տ� �ִ� ��� �� ��
    private GameObject rearBG = null; // ���� �������� �ִ� ��� �� ��
    private Player playerCont;
    private const float END_POINT = -2.0f; // ��� �� ���� ������ ������ ���� ���� Ȯ���ϱ� ���� ������ const�� �����ؼ� ����� ���� ������ �ʴ´�.
    private const float BACKGROUND_GAP = 1.43f; // ��� �� ���� ��
    private int frontIndex = 0; // backgrounds���� ���� front�� ����Ű�� �ε���
    //private int backgroundCnt;

    // ���� ������Ʈ�� ����� �� ���Ŀ� ȣ��Ǵ� �Լ�
    private void Awake()
    {
        // backgrounds �迭�� �޸𸮸� �Ҵ�.
        // transform.childCount�� ��ϵ� ������ŭ GameObject�� ���� �� �ִ� ũ��� �޸� �Ҵ�
        backgrounds = new GameObject[transform.childCount];
        playerCont = GameObject.Find("Player").GetComponent<Player>();

        // for���� �̿��� transform.childCount��ŭ �ݺ�
        for (int i = 0; i < transform.childCount; i++)
        {
            // �� ��ũ��Ʈ�� �ڽĵ��� ����(�޸� �ּ�)�� �ϳ��� backgrounds �迭�� ����
            backgrounds[i] = transform.GetChild(i).gameObject;
        }

        // ù ��° ��� �� ���� frontBG�� ����
        frontBG = backgrounds[0];
        // ������ ��� �� ���� rearBG�� ����
        rearBG = backgrounds[transform.childCount - 1];
    }

    // �� �����Ӹ��� ȣ��(Call)�ȴ�
    private void Update()
    {
        if (playerCont.isOnGround) return;
        // for���� �̿��� �ݺ� ����. i�� 0���� transform.childCound -1���� �����
        for (int i = 0; i < transform.childCount; i++)
        {
            // Transflate �Լ��� ����ؼ� backgrounds�� ��� �ִ� ��� ����� ���� ������
            // �����̴¹����� �����̰� 1�ʿ� scrollSpeed��ŭ �����δ�.
            backgrounds[i].transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
        }

        // ���ǹ��� ���� ���� �տ� �ִ� x���� END_POINT���� �۴� -> ������ ���� ȭ�鿡 ���� ���� ����.
        if (frontBG.transform.position.x < END_POINT)
        {
            // ���� �տ� �ִ� ��� �� ���� ���� �������� �ִ� ��� �� �� �ڿ� ���δ�.
            frontBG.transform.position = new Vector3(
                rearBG.transform.position.x + BACKGROUND_GAP,
                frontBG.transform.position.y,
                0.0f);

            // ���ο� ���� �հ� ���� �ڸ� ����
            rearBG = frontBG; // ���� ���� �տ� �ִ� ���� ���� �ڷ� �̵������� frontBG�� rearBG�� �Ҵ�

            // frontBG = backgrounds[++backgroundCnt % (transform.childCount)];
            // frontBG = backgrounds[++frondIndex];
            
            // �迭 ũ�Ⱑ 6�϶� ���� ������ ������ 0~5
            // �� ������ ����� �Ǹ� OutOfRangeException
            // ���ܸ� �����ϱ� ���� %�������� ���� ������ �����Ѵ�.
            // frontBG�� frontIndex���� �����ؼ� ����
            frontBG = backgrounds[++frontIndex % (transform.childCount)];
        }
    }
}
