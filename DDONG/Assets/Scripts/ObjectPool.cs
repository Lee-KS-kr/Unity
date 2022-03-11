using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject[] poopPrefabs;
    [SerializeField] private int firstInitCount = 40;
    private Queue<GameObject>[] poopPools = null;
    private static ObjectPool instance;

    #endregion

    #region Properties
    public static ObjectPool Inst
    {
        get => instance;
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        instance = this;
        Initialize(firstInitCount);
    }
    #endregion

    #region Helper Methods
    private void Initialize(int initCount)
    {
        poopPools = new Queue<GameObject>[poopPrefabs.Length];
        for(int i = 0; i < poopPrefabs.Length; i++)
        {
            poopPools[i] = new Queue<GameObject>();
            for(int j = 0; j < initCount; j++)
            {
                GameObject obj = Instantiate(poopPrefabs[i], this.transform);
                obj.SetActive(false);
                obj.name = $"{obj.name}_{j}";
                poopPools[i].Enqueue(obj);
            }
        }
    }

    private void ExpandPools(int poopIndex)
    {
        int makeSize = firstInitCount << 1;
        for(int i = 0; i < makeSize; i++)
        {
            GameObject obj = Instantiate(poopPrefabs[poopIndex], this.transform);
            obj.SetActive(false);
            obj.name = $"{obj.name}_{poopPools[poopIndex].Count + i}";
            poopPools[poopIndex].Enqueue(obj);
        }
    }

    public static GameObject GetPoop(int poopIndex)
    {
        if (instance.poopPools[poopIndex].Count > 0)
        {
            GameObject obj = instance.poopPools[poopIndex].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            instance.ExpandPools(poopIndex);
            GameObject obj = instance.poopPools[poopIndex].Dequeue();
            return obj;
        }
    }

    public static void ReturnObject(GameObject obj, int poopIndex)
    {
        obj.gameObject.SetActive(false);
        obj.transform.parent = instance.transform;
        instance.poopPools[poopIndex].Enqueue(obj);
    }
    #endregion
}
