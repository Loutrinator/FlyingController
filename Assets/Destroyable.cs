using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Destroyable : IPooledObject
{
    [SerializeField] private float maxHp;
    [SerializeField] private GameObject onDestroyEffect;
    private float currentHp;
    
    public void Hit(int damages)
    {
        Debug.Log("Hit !");
        currentHp -= damages;
        if (currentHp <= 0)
        {
            Disable();
        }
    }

    private void Disable()
    {
        Instantiate(onDestroyEffect, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    public override void OnObjectSpawn()
    {
        currentHp = maxHp;
    }
}
