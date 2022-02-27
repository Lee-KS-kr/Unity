using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    public float firstSpawn;
    public float spawnInterval;

    private void Start()
    {
        firstSpawn = 0;
        spawnInterval = 1.5f;
    }
    // Update is called once per frame
    void Update()
    {
        firstSpawn += Time.deltaTime;
        if (firstSpawn < spawnInterval) return;
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            firstSpawn = 0;
        }
    }
}
