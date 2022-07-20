using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Laser : IPooledObject
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float timeAlive = 5f;

    private float startTime;

    private void FixedUpdate()
    {
        transform.position += transform.forward * (speed * Time.fixedDeltaTime);
        if (Time.time - startTime > timeAlive || transform.position.y < -2f)
        {
            Disable();
        }
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Laser Hit");
        Destroyable destroyable = other.gameObject.GetComponent<Destroyable>();
        if (destroyable != null)
        {
            destroyable.Hit(1);
        }
    }

    public override void OnObjectSpawn()
    {
        startTime = Time.time;
    }
}
