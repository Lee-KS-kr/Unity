using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour
{
    public GameObject bullet; // 발사할 총알 오브젝트
    public Transform spawner; // 총알이 발사될 위치
    public Transform usedBullet; // 총알이 들어갈 parent gameObject
    
    public float interval = 1.0f; // 총알을 발사하는 간격(방아쇠를 당기는 간격)
    public float rateOfFire = 0.1f; // 연사 간격
    public int shots = 5; // 한번 발사할 때 몇연사를 할 것인가
    private bool isShoot = false;

    private IEnumerator shotSave;

    private void Awake()
    {
        shotSave = Shot();
    }
    
    public void Initialize(float _interval, int _shots, float _rateOfFire)
    {
        this.interval = _interval;
        this.shots = _shots;
        this.rateOfFire = _rateOfFire;
    }

    public void StartFire()
    {
        if (!isShoot)
        {
            StartCoroutine(shotSave);
            isShoot = true;
        }
    }

    public void StopFire()
    {
        StopCoroutine(shotSave);
        isShoot = false;
    }

    private IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval - shots * rateOfFire); // 1초 - 0.1*발사 횟수만큼 대기
            for (int i = 0; i < shots; i++)
            {
                GameObject bulletInstance = Instantiate(bullet, spawner.position, Quaternion.identity, usedBullet);
                //bulletInstance.transform.parent = null;
                yield return new WaitForSeconds(rateOfFire); // 0.1초 대기
            }
        }
    }
}
