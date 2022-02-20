using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f; //탄환 이동 속력
    private Rigidbody bulletRigidbody; //이동에 사용할 리지드 바디 컴포넌트

    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();

        //bulletRigidbody.velocity = Vector3.forward; //절대적인 화면 기준 앞으로 날아감
        bulletRigidbody.velocity = this.transform.forward * speed; //로컬 기준 내가 바라보는 앞으로 감

        Destroy(gameObject, 3f); //3초 후에 자신의 게임 오브젝트 파괴
    }

    private void OnTriggerEnter(Collider other)
    {
        //충돌한 상대방 게임 오브젝트가 Player태그를 가진 경우
        if(other.tag == "Player")
        {
            //상대방 게임 오브젝트에서 PlayerController 컴포넌트 가져오기
            PlayerController playerController = other.GetComponent<PlayerController>();

            // 상대방으로부터 PlayerController 컴포넌트를 가져오는데 성공하면
            if(playerController != null)
            {
                // 상대방 PlayerController 컴포넌트의 Die() 메서드를 실행
                playerController.Die();
            }
        }
    }


}
