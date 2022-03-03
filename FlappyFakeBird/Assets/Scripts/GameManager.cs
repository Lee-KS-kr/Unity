using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    private int userScore = 0;
    private Player playerController;

    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (!playerController.isGameOver)
        {
            userScore += (int)Time.deltaTime;
            scoreText.text = "Score : " + userScore;
        }
    }
}
