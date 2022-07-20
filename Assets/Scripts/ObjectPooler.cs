using System;
using System.Collections.Generic;
using UnityEngine;
public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public IPooledObject prefab;
        public int size;
    }
    
    #region Singleton
    public static ObjectPooler Instance
    {
        get
        {
            return _instance;
        }
    }

    private static ObjectPooler _instance;

    private void Awake()
    {
        _instance = this;
    }
    #endregion
    
    public List<Pool> pools;
    public Dictionary<string, Queue<IPooledObject>> poolDictionary;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<IPooledObject>>();
        foreach (var pool in pools)
        {
            Queue<IPooledObject> objectPool = new Queue<IPooledObject>();
            for (int i = 0; i < pool.size; i++)
            {
                IPooledObject go = Instantiate(pool.prefab);
                go.gameObject.SetActive(false);
                objectPool.Enqueue(go);
            }

            poolDictionary.Add(pool.tag,objectPool);
        }
    }

    public IPooledObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogError("A pool with tag '" + tag + "' does not exist.");
            return null;
        }
        IPooledObject pooledObject = poolDictionary[tag].Dequeue();
        pooledObject.gameObject.SetActive(true);
        Transform t = pooledObject.transform;
        t.position = position;
        t.rotation = rotation;
        pooledObject.OnObjectSpawn();
        poolDictionary[tag].Enqueue(pooledObject);
        return pooledObject;
    }
}