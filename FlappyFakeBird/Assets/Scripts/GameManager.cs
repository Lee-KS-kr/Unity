using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI text를 사용하기 위한 네임스페이스
using System.IO;

public class GameManager : MonoBehaviour
{
    public Text scoreText; // text UI
    public int userScore = 0; //text UI에 점수(숫자)를 넣기 위한 
    private Player playerController; // 게임오버 여부를 가져오기 위함
    private Teacher enemySpawner; // 레이캐스트를 통해 점수 상승
    private float scoreCheck = 0.5f; // 점수 체크 간격
    public int enemyScore = 0; // 적의 마리수를 저장하기 위한 변수
    private Vector2 startVector = new Vector2(1.0f, 1.5f); // 라인캐스트의 시작벡터
    private Vector2 endVector = new Vector2(1.0f, -0.9f); // 라인캐스트의 끝벡터

    private void Awake()
    {
        playerController = GameObject.FindObjectOfType<Player>(); // 플레이어 컨트롤러
        enemySpawner = GameObject.FindObjectOfType<Teacher>(); // 에너미스포너
        StartCoroutine(ScoreUp()); //점수 증가를 위해 코루틴 사용
    }

    void Update()
    {
        if (playerController.isGameOver) return; // 게임오버가 되면 점수상승x
        //scoreText.text = "Score : " + userScore; 
        scoreText.text = $"Score : {userScore}"; // text UI에 점수 상승
    }

    IEnumerator ScoreUp() // 점수 누적용 코루틴
    {
        while (true) // 계속해서 확인해야하기 때문에 무한루프
        {
            // 플레이어의 뒤를 지나갔는지 확인하여 라인캐스트
            RaycastHit2D hit = Physics2D.Linecast(startVector, endVector); 
            if (hit.collider != null) // 콜라이더가 감지
            {
                userScore += enemySpawner.GetQueue(); // 에너미스포너 스크립트에서 Dequeue
                yield return new WaitForSeconds(scoreCheck); // 일정 시간 대기
            }
            yield return null;
        }
    }

    public void SaveGameData()
    {
        SaveData saveData = new SaveData();
        saveData.highScore = 123;
        saveData.test1 = 11.22f;
        saveData.test2 = "Mizue_Lee";
        string json = JsonUtility.ToJson(saveData);
        string path = $"{Application.dataPath}/Save/Save.json";
        File.WriteAllText(path, json);
    }

    public void LoadGameData()
    {
        string path = $"{Application.dataPath}/Save/Save.json";
        string json = File.ReadAllText(path);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
    }
}
