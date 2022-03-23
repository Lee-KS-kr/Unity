using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CloseWeaponController : MonoBehaviour
// 미완성 클래스, 추상클래스
{
    [SerializeField] private CloseWeapon currentCloseWeapon;

    // 공격중
    protected bool isAttack = false;
    protected bool isSwing = false;

    protected RaycastHit hitInfo;

    protected void TryAttack()
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

    protected IEnumerator AttackCoroutine()
    {
        isAttack = true;
        currentCloseWeapon.animator.SetTrigger("Attack");

        yield return new WaitForSeconds(currentCloseWeapon.attackDelayOn);
        isSwing = true;

        StartCoroutine(HitCoroutine());

        yield return new WaitForSeconds(currentCloseWeapon.attackDelayOff);
        isSwing = false;

        yield return new WaitForSeconds(currentCloseWeapon.attackDelay - currentCloseWeapon.attackDelayOn - currentCloseWeapon.attackDelayOff);

        isAttack = false;
    }

    // 미완성. 추상 코루틴
    protected abstract IEnumerator HitCoroutine();

    protected bool CheckObject()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, currentCloseWeapon.range))
        {
            return true;
        }
        return false;
    }

    public virtual void CloseWeaponChange(CloseWeapon closeWeapon)
    {
        if (WeaponManager.currentWeapon != null)
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        }

        currentCloseWeapon = closeWeapon;
        WeaponManager.currentWeapon = currentCloseWeapon.transform;
        WeaponManager.currentWeaponAnimator = currentCloseWeapon.animator;

        currentCloseWeapon.transform.localPosition = Vector3.zero;
        currentCloseWeapon.gameObject.SetActive(true);
    }
}
