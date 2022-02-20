using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f; //źȯ �̵� �ӷ�
    private Rigidbody bulletRigidbody; //�̵��� ����� ������ �ٵ� ������Ʈ

    void Start()
    {
        bulletRigidbody = GetComponent<Rigidbody>();

        //bulletRigidbody.velocity = Vector3.forward; //�������� ȭ�� ���� ������ ���ư�
        bulletRigidbody.velocity = this.transform.forward * speed; //���� ���� ���� �ٶ󺸴� ������ ��

        Destroy(gameObject, 3f); //3�� �Ŀ� �ڽ��� ���� ������Ʈ �ı�
    }

    private void OnTriggerEnter(Collider other)
    {
        //�浹�� ���� ���� ������Ʈ�� Player�±׸� ���� ���
        if(other.tag == "Player")
        {
            //���� ���� ������Ʈ���� PlayerController ������Ʈ ��������
            PlayerController playerController = other.GetComponent<PlayerController>();

            // �������κ��� PlayerController ������Ʈ�� �������µ� �����ϸ�
            if(playerController != null)
            {
                // ���� PlayerController ������Ʈ�� Die() �޼��带 ����
                playerController.Die();
            }
        }
    }


}
