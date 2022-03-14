using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageNumber : MonoBehaviour
{
    // 숫자를 하나 받아서 자식 오브젝트인 Num100, Num10, Num1의 스프라이트를 변경해 숫자를 표현하는 클래스

    private const int DIGIT_SIZE = 3; // 세자리로 표현 할 것이라고 기획단계에서 결정났기 때문에 3
    public Image[] digits = new Image[DIGIT_SIZE]; // 3자리 숫자를 표현할 이미지 3개
    public Sprite[] numberSprites = new Sprite[10]; // 10진수를 표현하는 이미지 10개
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
            int digitNum = tempNumber / divideNum; // 자리수 구하기
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
