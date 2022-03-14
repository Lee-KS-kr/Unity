using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    // 현재 장착된 Hand형 타입 무기
    [SerializeField] private Hand currentHand;

    // 공격중
    private bool isAttack = false;
    private bool isSwing = false;

    private RaycastHit hitInfo;

    private void Update()
    {
        TryAttack();
    }

    private void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isAttack)
            {
                // 코루틴 실행
                StartCoroutine(AttackCoroutine());
            }
        }
    }

    private IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentHand.animator.SetTrigger("Attack");

        yield return new WaitForSeconds(currentHand.attackDelayOn);
        isSwing = true;

        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentHand.attackDelayOff);
        isSwing = false;

        yield return new WaitForSeconds(currentHand.attackDelay - currentHand.attackDelayOn - currentHand.attackDelayOff);

        isAttack = false;
    }

    private IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }

    private bool CheckObject()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, currentHand.range))
        {
            isSwing = false;
            return true;
        }
        return false;
    }
}
