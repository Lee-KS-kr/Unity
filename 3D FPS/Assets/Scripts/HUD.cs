using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // 필요한 컴포넌트
    [SerializeField] private GunController gunController;
    private Gun currentGun;

    // 필요시 HUD 호출, 비활성화
    [SerializeField] private GameObject gameObjectBulletHUD;
    [SerializeField] private Text[] bulletText; // 총알 개수를 텍스트에 반영

    private void Update()
    {
        CheckBullet();
    }

    private void CheckBullet()
    {
        currentGun = gunController.GetGun();
        bulletText[0].text = $"{currentGun.carryBulletCount}";
        bulletText[2].text = $"{currentGun.reloadBulletCount}";
        bulletText[2].text = $"{currentGun.currentBulletCount}";
    }
}
