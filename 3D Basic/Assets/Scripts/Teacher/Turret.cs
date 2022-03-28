using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawner;
    
    public float interval = 1.0f;
    public float rateOfFire = 0.1f;
    public int shots = 5;

    private void Start()
    {
        StartCoroutine(Shot());
    }

    private IEnumerator Shot()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval - shots * rateOfFire);
            for (int i = 0; i < shots; i++)
            {
                Instantiate(bullet, spawner);
                yield return new WaitForSeconds(rateOfFire);
            }
        }
    }
}
