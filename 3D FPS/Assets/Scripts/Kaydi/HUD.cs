using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    // �ʿ��� ������Ʈ
    [SerializeField] private GunController gunController;
    private Gun currentGun;

    // �ʿ�� HUD ȣ��, ��Ȱ��ȭ
    [SerializeField] private GameObject gameObjectBulletHUD;
    [SerializeField] private Text[] bulletText; // �Ѿ� ������ �ؽ�Ʈ�� �ݿ�

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
