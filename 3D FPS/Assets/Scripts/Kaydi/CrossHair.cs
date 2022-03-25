using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject crosshairHUD; // �ʿ��Ҷ��� ǥ���ϵ��� ����
    [SerializeField] private GunController gunController;

    private float gunAccuracy; // ũ�ν���� ���¿� ���� ���� ��Ȯ��

    #region Animator Methods
    public void WalkingAnimation(bool _flag)
    {
        animator.SetBool("Walking", _flag);
    }

    public void RunningAnimation(bool _flag)
    {
        animator.SetBool("Running", _flag);
    }

    public void CrouchingAnimation(bool _flag)
    {
        animator.SetBool("Crouching", _flag);
    }
    
    public void FineSightAnimation(bool _flag)
    {
        animator.SetBool("FineSight", _flag);
    }

    public void FireAnimation()
    {
        if (animator.GetBool("Walking"))
        {
            animator.SetTrigger("WalkFire");
        }
        else if (animator.GetBool("Crouching"))
        {
            animator.SetTrigger("CrouchFire");
        }
        else
        {
            animator.SetTrigger("IdleFire");
        }
    }
    #endregion

    public float GetAccuacy()
    {
        if (animator.GetBool("Walking"))
        {
            gunAccuracy = 0.08f;
        }
        else if (animator.GetBool("Crouching"))
        {
            gunAccuracy = 0.02f;
        }
        else if (gunController.GetFineSightMode())
        {
            gunAccuracy = 0.001f;
        }
        else
        {
            gunAccuracy = 0.04f;
        }

        return gunAccuracy;
    }
}
