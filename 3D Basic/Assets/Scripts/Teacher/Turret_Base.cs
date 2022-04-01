using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_Base : MonoBehaviour
{
    protected Gun[] _guns;
    
    [Header("Gun 생성용 변수")] [Range(1, 7)] public int gunCount = 0; // 발사할 총구의 갯수
    public GameObject gunPrefab; // 생성할 총구의 프리팹
    public Vector3 _standardPosition;
    public float interval = 1;
    public int shots = 3;
    public float rateOfFire = 0.1f;
    
    protected float _gunsDistance = 0.3f;

    protected void Initialize()
    {
        StartCoroutine(IntantiateGuns());
        _guns = transform.GetComponentsInChildren<Gun>();
        InitializeGun(interval, shots, rateOfFire);
    }
    
    protected void InitializeGun(float _interval, int _shots, float _rateOfFire)
    {
        for (int i = 0; i < _guns.Length; i++)
        {
            _guns[i].Initialize(_interval, _shots, _rateOfFire);
        }
    }
    
    protected IEnumerator IntantiateGuns()
    {
        for (int i = 0; i < gunCount; i++)
        {
            GameObject gunObject = Instantiate(gunPrefab, _standardPosition, Quaternion.identity, transform);
            switch (i)
            {
                case 0:
                    break;
                case 1:
                    gunObject.transform.Translate(- _gunsDistance, 0, 0);
                    break;
                case 2:
                    gunObject.transform.Translate(+ _gunsDistance, 0, 0);
                    break;
                case 3:
                    gunObject.transform.Translate(- (_gunsDistance / 2), _gunsDistance, 0);
                    break;
                case 4:
                    gunObject.transform.Translate(+ (_gunsDistance / 2), _gunsDistance, 0);
                    break;
                case 5:
                    gunObject.transform.Translate(- (_gunsDistance / 2), -_gunsDistance, 0);
                    break;
                case 6:
                    gunObject.transform.Translate(+ (_gunsDistance / 2), -_gunsDistance, 0);
                    break;
            }
        }
        yield break;
    }
    
    protected void StartFire()
    {
        for (int i = 0; i < _guns.Length; i++)
        {
            _guns[i].StartFire();
        }
    }

    protected void StopFire()
    {
        for (int i = 0; i < _guns.Length; i++)
        {
            _guns[i].StopFire();
        }
    }
}
