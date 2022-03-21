using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static bool isChangeWeapon = false;
    public static Transform currentWeapon;
    public static Animator currentWeaponAnimator;
    [SerializeField] private string currentWeaponType; // 현재 무기의 타입

    [SerializeField] private float changeWeaponDelay;
    [SerializeField] private float changeWeaponEndDelay;

    // 무기종류들을 모두 관리
    [SerializeField] private Gun[] guns;
    [SerializeField] private Hand[] hands;

    // 관리차원에서 쉽게 무기접근이 가능하도록 만듬.
    private Dictionary<string, Gun> gunDictionary = new Dictionary<string, Gun>();
    private Dictionary<string, Hand> handDictionary = new Dictionary<string, Hand>();

    // 필요한 컴포넌트
    [SerializeField] private GunController gunController;
    [SerializeField] private HandController handController;

    private void Start()
    {
        for(int i=0; i < guns.Length; i++)
        {
            gunDictionary.Add(guns[i].gunName, guns[i]);
        }
        for (int i = 0; i < hands.Length; i++)
        {
            handDictionary.Add(hands[i].handName, hands[i]);
        }
    }

    private void Update()
    {
        if (!isChangeWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartCoroutine(ChangeWeapon("HAND", "맨손"));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                StartCoroutine(ChangeWeapon("GUN", "SubMachinegun1"));
            }
        }
    }

    public IEnumerator ChangeWeapon(string _type, string _name)
    {
        isChangeWeapon = true;
        currentWeaponAnimator.SetTrigger("weaponOut");

        yield return new WaitForSeconds(changeWeaponDelay);
        
        CancelPreWeapon();
        WeaponChange(_type, _name);

        yield return new WaitForSeconds(changeWeaponEndDelay);
        currentWeaponType = _type;
        isChangeWeapon = false;
    }

    private void CancelPreWeapon()
    {
        switch (currentWeaponType)
        {
            case "GUN":
                gunController.CancleFineSight();
                gunController.CancelReload();
                break;
            case "HAND":
                break;
        }
    }

    private void WeaponChange(string _type, string _name)
    {
        if (_type == "GUN")
        {
            gunController.GunChange(gunDictionary[_name]);
        }
        else if (_type == "HAND")
        {
            handController.HandChange(handDictionary[_name]);
        }
    }
}
