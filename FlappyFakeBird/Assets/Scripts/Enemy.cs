using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 구현할 내용
    // 이 게임 오브젝트는 게임이 시작되면 -x축 방향으로 일정한 속도로 이동한다.
    // 이동하는 속도는 moveSpeed라는 변수에 저장되어 있다.
    public float moveSpeed = 1.5f;
    private Rigidbody2D enemyRb;


    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5.5f); // Instantiate 이후 5.5초 후에 삭제됨

    }

    private void Update()
    {
        // enemy의 x벡터를 마이너스 방향으로 이동...
        // 현재 위치에서 파라메터로 입력받은 만큼 조금씩 움직인다.
        transform.Translate(Vector2.left * Time.deltaTime * moveSpeed);
    }
}
