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
    [SerializeField] private Transform itemPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        Spawn(itemPrefab);
    }

    public void Spawn(Transform item)
    {
        for (int count = 0; count < amount; count++)
        {
            Vector2 horizontalPos = Random.insideUnitCircle * 100f;//rcle(100f);
            float x = Random.Range(minPos.x, maxPos.x);
            float y = Random.Range(minPos.y, maxPos.y);
            float z = Random.Range(minPos.z, maxPos.z);
            Vector3 pos = new Vector3(x, y, z);
            Instantiate(itemPrefab, pos, Quaternion.identity);
        }
    }
}
