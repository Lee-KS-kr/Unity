using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager
{
    private static GameManager instance = null; //프로그램 전체에서 단 하나만 존재한다.
    private GameManager() { }   //생성자를 private으로 해서 밖에서 new를 할 수 없도록 한다.
    

    private int highScore = 0;
    public int HighScore
    {
        get
        {
            return highScore;
        }
    }

    private int coinCount = 0;
    public int CoinCount
    {
        get
        {
            return coinCount;
        }
        set
        {
            coinCount = value;
            uiCoinCount.text = $"Coin : {coinCount}";
        }
    }

    private const float TIME = 30.0f;
    private float remindTime = 30.0f;

    Player player = null;
    public Player MyPlayer
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
        }
    }
    FollowCamera camera = null;

    public static GameManager Inst
    {
        get
        {
            if (instance == null)  // 객체 생성이 한번도 안일어났는지 확인
            {
                instance = new GameManager();   // 한번도 안일어났으면 그때 처음으로 객체 생성                
                instance.Initialize();
            }
            return instance;    //return까지 왔다는 것은 instance에 이미 무엇인가 할당이 되어있음
        }
    }

    GameObject uiClear = null;
    GameObject uiGameOver = null;
    Text uiCoinCount = null;
    Text uiTimer = null;
    private Text uiScore = null;

    void Initialize()
    {
        uiClear = GameObject.Find("GameClear");
        uiGameOver = GameObject.Find("GameOver");

        GameObject uiMain = GameObject.Find("MainUI");
        uiCoinCount = uiMain.transform.Find("CoinCount").GetComponent<Text>();
        uiTimer = uiMain.transform.Find("Timer").GetComponent<Text>();
        uiScore = GameObject.Find("ScoreText").GetComponent<Text>();

        LoadGameData();
    }

    public void Update(float deltaTime)
    {
        remindTime -= deltaTime;
        if(remindTime < 0)
        {
            // 게임 오버 처리
            OnGameOver();
            remindTime = 0;
        }

        uiTimer.text = $"Time : {remindTime:F2}";
    }


    public void SaveGameData()
    {
        SaveData saveData = new SaveData();
        saveData.highScore = highScore;
        string json = JsonUtility.ToJson(saveData); //SaveData 클래스에 있는 값들을 json형식으로 바꿔라
        //Debug.Log(json);

        string path = $"{Application.dataPath}/Save/";

        if (!Directory.Exists(path))   //path라는 폴더가 있는지 확인. Exists의 리턴이 true면 해당 폴더가 있다.
        {
            Directory.CreateDirectory(path);    //폴더가 없으면 만든다.
        }

        string fullPath = $"{path}Save.json";
        File.WriteAllText(fullPath, json);  //path에 json텍스트를 실제 파일로 저장 
    }

    public void LoadGameData()
    {
        string path = $"{Application.dataPath}/Save/";
        string fullPath = $"{path}Save.json";

        if (Directory.Exists(path) && File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);   //path 파일에 있는 텍스트를 읽어서 json변수에 스트링으로 저장
            SaveData saveData = JsonUtility.FromJson<SaveData>(json); //json형식의 텍스트를 SaveData 클래스에 담기
            highScore = saveData.highScore;
            Debug.Log($"highScore : {saveData.highScore}");
        }
    }

    //게임 오버될 때 실행할 함수
    public void OnGameOver()
    {
        //if (score > highScore)    //하이스코어 일때 처리
        //{
        //    highScore = score;      //하이스코어 갱신하고 
        //    SaveGameData();         //파일로 저장
        //}

        player.OnDead();
        CanvasGroup group = uiGameOver.GetComponent<CanvasGroup>();
        group.alpha = 1.0f;

        camera.SetTarget(null);
    }

    public void OnStageStart()
    {
        player = GameObject.FindObjectOfType<Player>();
        camera = GameObject.FindObjectOfType<FollowCamera>();
        camera.SetTarget(player.GetCameraPosition());

        uiClear = GameObject.Find("GameClear");
        CanvasGroup group = uiClear.GetComponent<CanvasGroup>();
        group.alpha = 0.0f;

        coinCount = 0;
        remindTime = TIME;
    }

    public void OnStageClear()
    {
        CanvasGroup group = uiClear.GetComponent<CanvasGroup>();
        group.alpha = 1.0f;
        int coinScore = coinCount * 10;
        int timeScore = (int) remindTime * 10;
        int finalScore = coinScore + timeScore;
        uiScore.text =
            $"Coin : {coinCount} = {coinScore}\nTime : {(int) remindTime} = {timeScore}\nScore : {finalScore}";
    }

    public static void Test()
    {
        Debug.Log("Test");
    }
}