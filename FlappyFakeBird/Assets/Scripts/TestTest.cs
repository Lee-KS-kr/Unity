using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTest : MonoBehaviour
{
    //FindObjectOfType : Ư�� Ÿ���� ������Ʈ�� ������ �ִ� ���� ������Ʈ�� ã���ִ� �Լ�
    //Find : �Ķ���ͷ� ���� ���ڿ��� ���� �̸��� ���� ���� ������Ʈ�� ã���ִ� �Լ� (���� ��ȿ����)
    //FindGameObjectWithTag : �Ķ���ͷ� ���� ���ڿ��� ���� �±׸� ���� ���� ������Ʈ�� ã���ִ� �Լ�


    // �̱��� ����. Monobehaviour�� ��ӹ��� ��ũ��Ʈ������ ����� �Ұ����ϴ�.
    private static TestTest instance = null; // ���α׷� ��ü���� �� �ϳ��� �����Ѵ�.
    private TestTest() { } // �����ڸ� private���� �ؼ� �ۿ��� new �� �� ������ �Ѵ�.

    private static int score = 0;
    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    // ������Ƽ �ۼ�
    // public�̶� ���� �ۿ��� ������ �����ϴ�
    // static �Լ��� ��ü�� �ۿ��� �������� �ʾƵ� �Ǳ� ������ static���� ����

    public static TestTest Inst
    {
        get
        {
            if (instance == null) // ��ü ������ �ѹ��� ���Ͼ���� Ȯ��
            {
                instance = new TestTest(); // �ѹ��� ���Ͼ���� �׶� ó������ ��ü ����
            }
            return instance; // return���� �Դٴ� ���� instance�� �̹� �����ΰ� �Ҵ�Ǿ�����
        }
    }
}
