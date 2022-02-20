using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.IO;

public class GameManager : MonoBehaviour
{
    public string[] enemyObjs;
    public Transform[] spawnPoints;

    public float nextSpawnDelay;
    public float curSpawnDelay;

    public GameObject player;
    public Text scoreText;
    public Image[] lifeImage;
    public Image[] bombImage;
    public GameObject GameOverSet;
    public GameObject BossMassage;
    public ObjectManager objectManager;
    AudioSource audioSource;

    public List<Spawn> spwanList;
    public int spawnIndex;
    public bool spawnEnd;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        audioSource = GetComponent<AudioSource>();
        enemyObjs = new string[] { "EnemyL", "EnemyM", "EnemyS", "Boss"};
        spwanList = new List<Spawn>();
        ReadSpawnFile();
    }

    void ReadSpawnFile()
    {
        spwanList.Clear();
        spawnIndex = 0;
        spawnEnd = false;

        TextAsset textFile = Resources.Load("Stage0") as TextAsset;
        StringReader stringReader = new StringReader(textFile.text);

        while(stringReader != null)
        {
            string line = stringReader.ReadLine();
            Debug.Log(line);
            if (line == null)
                break;

            Spawn spawnData = new Spawn();
            spawnData.delay = float.Parse(line.Split(',')[0]);
            spawnData.type = line.Split(',')[1];
            spawnData.point = int.Parse(line.Split(',')[2]);

            spwanList.Add(spawnData);
        }
        stringReader.Close();

        nextSpawnDelay = spwanList[0].delay;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        curSpawnDelay += Time.deltaTime;
        if(curSpawnDelay > nextSpawnDelay && !spawnEnd)
        {
            SpawnEnemy();
            curSpawnDelay = 0;
        }

        Player playerLogic = player.GetComponent<Player>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);
    }

    void SpawnEnemy()
    {
        int enemyIndex = 0;
        switch(spwanList[spawnIndex].type)
        {
            case "S":
                enemyIndex = 2; break;
            case "M":
                enemyIndex = 1; break;
            case "L":
                enemyIndex = 0; break;
            case "B":
                enemyIndex = 3; audioSource.Stop(); break;
            case "W" :
                BossApearance(); break;
        }

        if (spwanList[spawnIndex].type != "W")
        {
            int enemyPoint = spwanList[spawnIndex].point;

            GameObject enemy = objectManager.MakeObj(enemyObjs[enemyIndex]);
            enemy.transform.position = spawnPoints[enemyPoint].position;

            Rigidbody2D rigid = enemy.GetComponent<Rigidbody2D>();
            Enemy enemyLogic = enemy.GetComponent<Enemy>();
            enemyLogic.player = player;
            enemyLogic.objectManager = objectManager;

            if (enemyPoint == 5 || enemyPoint == 7)
            {
                rigid.velocity = new Vector2((-1) * enemyLogic.speed, -1);
            }
            else if (enemyPoint == 6 || enemyPoint == 8)
            {
                rigid.velocity = new Vector2(enemyLogic.speed, -1);
            }
            else
            {
                rigid.velocity = new Vector2(0, enemyLogic.speed * (-1));
            }
        }
        spawnIndex++;
        if (spawnIndex == spwanList.Count)
        {
            spawnEnd = true;
            return;
        }

        nextSpawnDelay = spwanList[spawnIndex].delay;
    }

    public void RespawnPlayer()
    {
        Invoke("RespawnPlayerExe", 2f);
    }

    void RespawnPlayerExe()
    {
        player.transform.position = Vector3.down * 4;
        player.SetActive(true);
        Invoke("Revive",1f);
        Player playerLogic = GameObject.FindObjectOfType<Player>();
        playerLogic.isAlive = true;
    }

    void Revive()
    {
        player.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void UpdateLifeIcon(int life)
    {
        for (int index = 0; index < 3; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 0);
        }
        for (int index = 0; index < life; index++)
        {
            lifeImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void UpdateBombIcon(int bomb)
    {
        for (int index = 0; index < 3; index++)
        {
            bombImage[index].color = new Color(1, 1, 1, 0);
        }
        for (int index = 0; index < bomb; index++)
        {
            bombImage[index].color = new Color(1, 1, 1, 1);
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverSet.SetActive(true);
        audioSource.Stop();
        GameOverSet.GetComponent<AudioSource>().Play();
    }

    public void GameRetry()
    {
        Application.Quit();
        SceneManager.LoadScene(0);
    }

    public void BossApearance()
    {
        Debug.Log("보스온다.");
        BossMassage.SetActive(true);
        BossMassage.GetComponent<AudioSource>().Play();
        Invoke("BossOff", 20);
    }

    public void BossOff()
    {
        BossMassage.SetActive(false);
        BossMassage.GetComponent<AudioSource>().Stop();
    }
}
