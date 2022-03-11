using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private PoopSpawner spawner;
    [SerializeField] private PlayerController player;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject gameOverImage;

    private Vector2 startVector = new Vector2(-9,-3.5f);
    private Vector2 endVector = new Vector2(9, -3.5f);

    [SerializeField] private int userScore = 0;
    private float timeElapsed = 0;
    private float scoreCheck = 0.5f;
    #endregion

    #region Unity Methods
    private void Start()
    {
        StartCoroutine(ScoreUp());
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        timerText.text = $"Timer : {(int)timeElapsed}";
        if(player.isGameOver)
        {
            gameOverImage.SetActive(true);
            return;
        }
        scoreText.text = $"Score : {userScore}";
    }
    #endregion

    #region Helper Methods
    private IEnumerator ScoreUp()
    {
        while (true)
        {
            RaycastHit2D raycast = Physics2D.Linecast(startVector, endVector);
            if (raycast.collider != null)
            {
                userScore += spawner.GetScore();
                yield return new WaitForSeconds(scoreCheck);
            }
            yield return null;
        }
    }
    #endregion
}
