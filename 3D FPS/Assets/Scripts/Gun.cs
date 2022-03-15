using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName; // 총 이름

    public float range; // 총 사정거리
    public float accuracy; // 정확도
    public float fireRate; // 연사속도
    public float damage; // 총 데미지

    public float reloadTime; //재장전 속도
    public int reloadBulletCount; // 총알 재장전 갯수
    public int currentBulletCount; // 현재 탄알집에 남아있는 총알의 갯수
    public int maxBulletCount; // 최대 소유 가능 총알 갯수
    public int carryBulletCount; // 현재 소유중인 총알 갯수

    public float retroActionForce; // 반동 세기
    public float retroActionFineSightForce; // 정조준시 반동 세기

    public Vector3 fineSightOriginPos; // 정조준시 위치

    public Animator animator;
    public ParticleSystem muzzleFlash; // 총구 섬광
    public AudioClip fireSound; // 총 발사 소리
}
