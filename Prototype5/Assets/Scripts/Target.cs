using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float torqueSpeed = 10;
    private float ranPos = 4;
    private float yRandPos = -2;
    public int targetScore;

    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();

        targetRb.AddForce(Vector3.up * Random.Range(minSpeed, maxSpeed), ForceMode.Impulse);
        targetRb.AddTorque(Test(), ForceMode.Impulse);

        transform.position = new Vector3(Random.Range(-ranPos, ranPos), yRandPos);
    }

    //private Vector3 RandomVector()
    //{
    //    Vector3 vector = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
    //    return vector;
    //}

    Vector3 Test() => new Vector3(Random.Range(-torqueSpeed, torqueSpeed), Random.Range(-torqueSpeed, torqueSpeed), Random.Range(-torqueSpeed, torqueSpeed));

    private void OnMouseDown()
    {
        if (!gameManager.isGameActive) return;
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        Destroy(gameObject);
        gameManager.UpdateScore(targetScore);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }
}
