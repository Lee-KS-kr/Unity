using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FireBall : MonoBehaviour
{
    public float chargeStrength;
    [SerializeField] private GameObject ballPrefabs;
    [SerializeField] private GameObject fireHole;
    [SerializeField] private TextMeshProUGUI chargePower;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            chargeStrength += (10 * Time.deltaTime);
            if (chargeStrength > 30)
                chargeStrength -= 30;
            chargePower.text = $"Charge Power : {(int)chargeStrength}";
        }
    }

    private void OnMouseUp()
    {
        Instantiate(ballPrefabs, fireHole.transform.position, gameObject.transform.rotation);
        chargeStrength = 0;
    }
}
