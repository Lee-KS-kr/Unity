using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int totalPoint;
    public int stagePoint;
    public int stageIndex;
    public int health;
    public GameObject[] Stages;
    public PlayerMove player;
    public Image[] UIhealth;
    public Text UIPoint;
    public Text UIStage;
    public Text UIClear;
    public GameObject RestartButton;

    private void Update()
    {
        UIPoint.text = "POINT : " + (totalPoint + stagePoint).ToString();
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
    public void NextStage()
    {
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE " + stageIndex;
        }
        else
        {
            Time.timeScale = 0;
            UIClear.gameObject.SetActive(true);
            RestartButton.SetActive(true);
        }

        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        //if (--health == 0)
        //{
        //    player.Die();
        //    UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        //}
        if (health > 1)
        {
            health--;
            UIhealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            Invoke("TimeStop", 1f);
            RestartButton.SetActive(true);
        }
    }

    void TimeStop()
    {
        Time.timeScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (health > 1)
            {
                PlayerReposition();
            }
            HealthDown();
        }  
    }

    void PlayerReposition()
    {
        player.transform.position = new Vector3(-6, 4, -1);
        player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
