using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject enemyLPrefab;
    public GameObject enemyMPrefab;
    public GameObject enemySPrefab;
    public GameObject BossPrefab;

    public GameObject itemCoinPrefab;
    public GameObject itemPowerPrefab;
    public GameObject itemBoomPrefab;

    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletFollowerPrefab;
    public GameObject bulletEnemyAPrefab;
    public GameObject bulletEnemyBPrefab;
    public GameObject bulletBossAPrefab;
    public GameObject bulletBossBPrefab;

    GameObject[] enemyL;
    GameObject[] enemyM;
    GameObject[] enemyS;
    GameObject[] Boss;

    GameObject[] itemCoin;
    GameObject[] itemPower;
    GameObject[] itemBoom;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletFollower;
    GameObject[] bulletEnemyA;
    GameObject[] bulletEnemyB;
    GameObject[] bulletBossA;
    GameObject[] bulletBossB;

    GameObject[] targetPool;

    private void Awake()
    {
        enemyL = new GameObject[10];
        enemyM = new GameObject[10];
        enemyS = new GameObject[20];
        Boss = new GameObject[5];

        itemCoin = new GameObject[20];
        itemPower = new GameObject[10];
        itemBoom = new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletFollower = new GameObject[100];
        bulletEnemyA = new GameObject[100];
        bulletEnemyB = new GameObject[100];
        bulletBossA = new GameObject[100];
        bulletBossB = new GameObject[400];

        Generate();
    }

    void Generate()
    {
        GenerateObject(ref enemyL, enemyLPrefab);
        GenerateObject(ref enemyM, enemyMPrefab);
        GenerateObject(ref enemyS, enemySPrefab);
        GenerateObject(ref Boss, BossPrefab);

        GenerateObject(ref itemCoin, itemCoinPrefab);
        GenerateObject(ref itemPower, itemPowerPrefab);
        GenerateObject(ref itemBoom, itemBoomPrefab);

        GenerateObject(ref bulletPlayerA, bulletPlayerAPrefab);
        GenerateObject(ref bulletPlayerB, bulletPlayerBPrefab);
        GenerateObject(ref bulletFollower, bulletFollowerPrefab);
        GenerateObject(ref bulletEnemyA, bulletEnemyAPrefab);
        GenerateObject(ref bulletEnemyB, bulletEnemyBPrefab);
        GenerateObject(ref bulletBossA, bulletBossAPrefab);
        GenerateObject(ref bulletBossB, bulletBossBPrefab);
        
    }

    private void GenerateObject(ref GameObject[] gameObject, GameObject prefab)
    {
        for (int index = 0; index < gameObject.Length; index++)
        {
            gameObject[index] = Instantiate(prefab, transform);
            gameObject[index].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "EnemyL": 
                targetPool = enemyL;
                break;
            case "EnemyM": 
                targetPool = enemyM; 
                break;
            case "EnemyS": 
                targetPool = enemyS; 
                break;
            case "Boss": 
                targetPool = Boss; 
                break;
            case "itemCoin": 
                targetPool = itemCoin; 
                break;
            case "itemPower": 
                targetPool = itemPower; 
                break;
            case "itemBoom": 
                targetPool = itemBoom; 
                break;
            case "bulletPlayerA": 
                targetPool = bulletPlayerA; 
                break;
            case "bulletPlayerB": 
                targetPool = bulletPlayerB; 
                break;
            case "bulletFollower": 
                targetPool = bulletFollower; 
                break;
            case "bulletEnemyA": 
                targetPool = bulletEnemyA; 
                break;
            case "bulletEnemyB": 
                targetPool = bulletEnemyB; 
                break;
            case "bulletBossA": 
                targetPool = bulletBossA; 
                break;
            case "bulletBossB": 
                targetPool = bulletBossB; 
                break;
            default: break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }

        return null;
    }

    public GameObject[] GetPool(string type) 
    {
        switch (type)
        {
            case "EnemyL":
                targetPool = enemyL;
                break;
            case "EnemyM":
                targetPool = enemyM;
                break;
            case "EnemyS":
                targetPool = enemyS;
                break;
            case "Boss":
                targetPool = Boss;
                break;
            case "itemCoin":
                targetPool = itemCoin;
                break;
            case "itemPower":
                targetPool = itemPower;
                break;
            case "itemBoom":
                targetPool = itemBoom;
                break;
            case "bulletPlayerA":
                targetPool = bulletPlayerA;
                break;
            case "bulletPlayerB":
                targetPool = bulletPlayerB;
                break;
            case "bulletFollower":
                targetPool = bulletFollower;
                break;
            case "bulletEnemyA":
                targetPool = bulletEnemyA;
                break;
            case "bulletEnemyB":
                targetPool = bulletEnemyB;
                break;
            case "bulletBossA":
                targetPool = bulletBossA;
                break;
            case "bulletBossB":
                targetPool = bulletBossB;
                break;
            default: break;
        }
        return targetPool;
    }
}
