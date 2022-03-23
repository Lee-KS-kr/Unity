using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool isDoorOpen = false; // 문 이동을 위한 변수. player에서 사용을 위해 public
    public bool isInCollider = false; // 콜라이더 내부에서 수동으로 조작을 위한 변수. player에서 사용을 위해 public
    private float _sizeOfDoor = 2.0f; // 문 이동시 참고하기 위한 문의 크기
    
    private Transform _doorTransform; // 문 이동시 사용하기 위한 변수
    private Vector3 _tempPosition; // 버그수정을 위한 벡터변수

    // 자식객체 문 찾아서 저장
    private void Awake()
    {
        _doorTransform = transform.Find("Door");
        _tempPosition = _doorTransform.position;
    }
    
    // 콜라이더에 들어가면 자동으로 문이 열리게 함
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        isDoorOpen = true;
        MoveDoor();
    }
    
    // 콜라이더 내부에 있는 동안 player가 space key를 누르면 수동으로 문을 조작할 수 있도록
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInCollider = true;
        }
    }

    // 콜라이더에서 빠져나가면 수동조작 종료, 문을 닫히게 함
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        isDoorOpen = false;
        isInCollider = false;
        MoveDoor();
    }

    public void MoveDoor()
    {
        if (isDoorOpen) // 문 열기
        {
            _doorTransform.Translate(_sizeOfDoor, 0, 0);
        }
        else // 문 닫기
        {
            if (_doorTransform.position == _tempPosition) return; // 문이 닫힌상태에서 움직이지 않게 하기 위한 코드
            _doorTransform.Translate(-_sizeOfDoor,0,0);
        }
    }
}
