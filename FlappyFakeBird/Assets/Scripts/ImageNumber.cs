using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageNumber : MonoBehaviour
{
    // ���ڸ� �ϳ� �޾Ƽ� �ڽ� ������Ʈ�� Num100, Num10, Num1�� ��������Ʈ�� ������ ���ڸ� ǥ���ϴ� Ŭ����

    private const int DIGIT_SIZE = 3; // ���ڸ��� ǥ�� �� ���̶�� ��ȹ�ܰ迡�� �������� ������ 3
    public Image[] digits = new Image[DIGIT_SIZE]; // 3�ڸ� ���ڸ� ǥ���� �̹��� 3��
    public Sprite[] numberSprites = new Sprite[10]; // 10������ ǥ���ϴ� �̹��� 10��
    private GameManager gameManager;
    private Player playerController;
    public int number = 0;
    private float scoreCheck = 0.01f; 

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerController = FindObjectOfType<Player>();
        StartCoroutine(UpdateScore());
    }

    public void MakeImageNumber()
    {
        if (number > 999)
        {
            number = 999;
        }

        int tempNumber = number;
        int divideNum = 100;
        for(int i = 0; i < DIGIT_SIZE; i++)
        {
            int digitNum = tempNumber / divideNum; // �ڸ��� ���ϱ�
            tempNumber %= divideNum;
            divideNum /= 10;

            digits[i].sprite = numberSprites[digitNum];
        }
    }

    private IEnumerator UpdateScore()
    {
        while(!playerController.isGameOver)
        {
            yield return new WaitForSeconds(scoreCheck); 
            number = gameManager.userScore; 
            MakeImageNumber();
        }
    }

    //private void OnValidate()
    //{
    //    Debug.Log("OnValidate");
    //    MakeImageNumber();
    //}
}
