using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopSpawner : MonoBehaviour
{
    #region Variables
    public GameObject[] poopPrefabs;

    private int spawnNumber;
    private int minSpawn = 3;
    private int maxSpawn = 18;
    private int spawnPositionIndex = 9;

    [SerializeField] private float spawnDelay = 2;
    [SerializeField] private float destroyDelay = 2.5f;

    private Queue scoreQueue = new Queue();
    #endregion

    #region Unity Methods
    private void Start()
    {
        StartCoroutine(SpawnPoops());
    }
    #endregion

    #region Helper Methods
    private IEnumerator SpawnPoops()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            spawnNumber = Random.Range(minSpawn, maxSpawn);

            for (int index = 0; index < spawnNumber; index++)
            {
                int spawnIndex = Random.Range(0, poopPrefabs.Length);
                //GameObject obj = Instantiate(poopPrefabs[spawnIndex], this.transform);
                GameObject obj = ObjectPool.GetPoop(spawnIndex);
                obj.transform.parent = this.transform;
                int spawnPosIndex = Random.Range(-spawnPositionIndex, spawnPositionIndex);
                obj.transform.position = new Vector2((this.transform.position.x + spawnPosIndex), transform.position.y);

                //Destroy(obj, destroyDelay);
                StartCoroutine(Return(obj, spawnIndex));
            }

            scoreQueue.Enqueue(spawnNumber);
        }
    }

    private IEnumerator Return(GameObject obj, int poopIndex)
    {
        yield return new WaitForSeconds(destroyDelay);
        ObjectPool.ReturnObject(obj, poopIndex);
    }

    public int GetScore()
    {
        return (int)scoreQueue.Dequeue();
    }
    #endregion
}
