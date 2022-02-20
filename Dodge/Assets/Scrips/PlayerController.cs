using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))] //강제로 rigidbody component를 끌고 옴 없으면 에러나니까
public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody; //이동에 사용할 rigidbody 컴포넌트
    public float speed = 8f; //캐릭터의 이동 속도

    private void Start()
    {
        // <T> C# 제네릭
        playerRigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        /*
         * Input.GetKey() : 키보드의 식별자를 KeyCode 타입으로 입력받는다
         * 키를 누르고 있으면 true, 그렇지 않으면 false를 반환한다
         * Input.GetKeyDown() : 해당 키를 누르는 순간 true
         * Input.GetKeyUp() : 해당 키를 누르다가 떼는 순간 true
         */

        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;
        if (Input.GetKey(KeyCode.Space))
        {
            playerRigidbody.AddForce(0f, speed, 0f);
        }
        Vector3 newVelocity = new Vector3(xSpeed, 0f, zSpeed);

        playerRigidbody.velocity = newVelocity;

        //AddForce 매서드는 사용해 힘을 누적하고 속력을 점진적으로 증가시킨다(관성o)
        //Velocity를 수정하는 것은 이전 속도를 지우고 새로운 속도를 사용하는 것이다.(관성x)

    }
        public void Die()
        {
            //자신의 게임 오브젝트를 비활성화
            gameObject.SetActive(false);

            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.EndGame();

        }
}