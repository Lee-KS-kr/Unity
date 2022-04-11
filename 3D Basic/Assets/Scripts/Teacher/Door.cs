using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Door_Teacher
{
    public float autoDoorClose = 3;

    public override bool Open()
    {
        // front가 false면 문 뒤에 플레이어가 있다.
        anim.SetTrigger("DoorOpenBack"); // 문을 뒤쪽에서 앞으로 열라는 트리거

        isOpen = true;
        StartCoroutine(AutoClose());

        return isOpen;
    }

    private IEnumerator AutoClose()
    {
        yield return new WaitForSeconds(autoDoorClose);
        Close();
    }
}