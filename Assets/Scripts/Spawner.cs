using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Vector3 minPos;
    [SerializeField] private Vector3 maxPos;
    [SerializeField] private int amount;
    [SerializeField] private string poolName;
    private ObjectPooler _objectPooler;
    
    // Start is called before the first frame update
    void Start()
    {
        _objectPooler = ObjectPooler.Instance;
        Spawn();
    }

    public void Spawn()
    {
        Quaternion rot = Quaternion.identity;
        for (int count = 0; count < amount; count++)
        {
            float x = Random.Range(minPos.x, maxPos.x);
            float y = Random.Range(minPos.y, maxPos.y);
            float z = Random.Range(minPos.z, maxPos.z);
            Vector3 pos = new Vector3(x, y, z);
            _objectPooler.SpawnFromPool(poolName, pos, rot);
        }
    }
}
