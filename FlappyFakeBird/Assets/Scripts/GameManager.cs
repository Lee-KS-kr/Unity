using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI text�� ����ϱ� ���� ���ӽ����̽�

public class GameManager : MonoBehaviour
{
    public Text scoreText; // text UI
    public int userScore = 0; //text UI�� ����(����)�� �ֱ� ���� 
    private Player playerController; // ���ӿ��� ���θ� �������� ����
    private Teacher enemySpawner; // ����ĳ��Ʈ�� ���� ���� ���
    private float scoreCheck = 0.5f; // ���� üũ ����
    public int enemyScore = 0; // ���� �������� �����ϱ� ���� ����
    private Vector2 startVector = new Vector2(1.0f, 1.5f); // ����ĳ��Ʈ�� ���ۺ���
    private Vector2 endVector = new Vector2(1.0f, -0.9f); // ����ĳ��Ʈ�� ������

    private void Awake()
    {
        playerController = GameObject.FindObjectOfType<Player>(); // �÷��̾� ��Ʈ�ѷ�
        enemySpawner = GameObject.FindObjectOfType<Teacher>(); // ���ʹ̽�����
        StartCoroutine(ScoreUp()); //���� ������ ���� �ڷ�ƾ ���
    }

    void Update()
    {
        if (playerController.isGameOver) return; // ���ӿ����� �Ǹ� �������x
        scoreText.text = "Score : " + userScore; // text UI�� ���� ���
    }

    IEnumerator ScoreUp() // ���� ������ �ڷ�ƾ
    {
        while (true) // ����ؼ� Ȯ���ؾ��ϱ� ������ ���ѷ���
        {
            // �÷��̾��� �ڸ� ���������� Ȯ���Ͽ� ����ĳ��Ʈ
            RaycastHit2D hit = Physics2D.Linecast(startVector, endVector); 
            if (hit.collider != null) // �ݶ��̴��� ����
            {
                userScore += enemySpawner.GetQueue(); // ���ʹ̽����� ��ũ��Ʈ���� Dequeue
                yield return new WaitForSeconds(scoreCheck); // ���� �ð� ���
            }
            yield return null;
        }
    }
}