using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))] //������ rigidbody component�� ���� �� ������ �������ϱ�
public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody; //�̵��� ����� rigidbody ������Ʈ
    public float speed = 8f; //ĳ������ �̵� �ӵ�

    private void Start()
    {
        // <T> C# ���׸�
        playerRigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        /*
         * Input.GetKey() : Ű������ �ĺ��ڸ� KeyCode Ÿ������ �Է¹޴´�
         * Ű�� ������ ������ true, �׷��� ������ false�� ��ȯ�Ѵ�
         * Input.GetKeyDown() : �ش� Ű�� ������ ���� true
         * Input.GetKeyUp() : �ش� Ű�� �����ٰ� ���� ���� true
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

        //AddForce �ż���� ����� ���� �����ϰ� �ӷ��� ���������� ������Ų��(����o)
        //Velocity�� �����ϴ� ���� ���� �ӵ��� ����� ���ο� �ӵ��� ����ϴ� ���̴�.(����x)

    }
        public void Die()
        {
            //�ڽ��� ���� ������Ʈ�� ��Ȱ��ȭ
            gameObject.SetActive(false);

            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.EndGame();

        }
}