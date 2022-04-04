// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Serialization;
//
// public class ObjectPool : MonoBehaviour
// {
//     #region Variables
//
//     [SerializeField] private GameObject bulletPrefab;
//     [SerializeField] private int firstInitCount = 40;
//     private Queue<GameObject> _bulletPool = null;
//     private static ObjectPool _instance;
//
//     #endregion
//
//     #region Properties
//
//     public static ObjectPool Inst
//     {
//         get => _instance;
//     }
//
//     #endregion
//
//     #region Unity Methods
//
//     private void Awake()
//     {
//         if (_instance == null) // 제일 처음 만들어진 인스턴스다.
//         {
//             _instance = this;
//             _instance.Initialize(firstInitCount);
//             DontDestroyOnLoad(this.gameObject); // 다른 씬이 로드되더라도 삭제되지 않는다.
//         }
//         else
//         {
//             // 이미 인스턴스가 만들어진게 있다.
//             if (_instance != this) // 이미 만들어진 것이 나와 다르다.
//             {
//                 Destroy(this.gameObject); //나는 죽는다.
//             }
//         }
//     }
//
//     #endregion
//
//     #region Helper Methods
//
//     private void Initialize(int initCount)
//     {
//         _bulletPool = new Queue<GameObject>();
//         for (int j = 0; j < initCount; j++)
//         {
//             GameObject obj = Instantiate(bulletPrefab, this.transform);
//             obj.SetActive(false);
//             obj.name = $"{obj.name}_{j}";
//             _bulletPool.Enqueue(obj);
//         }
//     }
//
//     private void ExpandPools()
//     {
//         int makeSize = firstInitCount << 1;
//         for (int i = 0; i < makeSize; i++)
//         {
//             GameObject obj = Instantiate(bulletPrefab, this.transform);
//             obj.SetActive(false);
//             obj.name = $"{obj.name}_{_bulletPool.Count + i}";
//             _bulletPool.Enqueue(obj);
//         }
//     }
//
//     public static GameObject GetBullet(int bulletIndex)
//     {
//         if (_instance._bulletPool.Count > 0)
//         {
//             GameObject obj = _instance._bulletPool.Dequeue();
//             obj.SetActive(true);
//             return obj;
//         }
//         else
//         {
//             _instance.ExpandPools();
//             GameObject obj = _instance._bulletPool.Dequeue();
//             return obj;
//         }
//     }
//
//     public static void ReturnObject(GameObject obj)
//     {
//         obj.gameObject.SetActive(false);
//         obj.transform.parent = _instance.transform;
//         _instance._bulletPool.Enqueue(obj);
//     }
//
//     #endregion
// }