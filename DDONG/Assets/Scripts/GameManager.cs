using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private PoopSpawner spawner;
    [SerializeField] private PlayerController player;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;
    [SerializeField] private GameObject gameOverImage;
    private AudioSource backGroundMusic;

    private Vector2 startVector = new Vector2(-9,-3.5f);
    private Vector2 endVector = new Vector2(9, -3.5f);

    [SerializeField] private int userScore = 0;
    private float timeElapsed = 0;
    private float scoreCheck = 0.5f;
    #endregion

    #region Unity Methods
    private void Start()
    {
        backGroundMusic = GetComponent<AudioSource>();
        StartCoroutine(ScoreUp());
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if(player.isGameOver)
        {
            backGroundMusic.Stop();
            gameOverImage.SetActive(true);
            player.gameObject.GetComponent<Collider2D>().isTrigger = true;
            return;
        }
        timerText.text = $"Timer : {(int)timeElapsed}";
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion
}
