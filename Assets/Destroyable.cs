using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Destroyable : MonoBehaviour
{
    [SerializeField] private float maxHp;
    private float currentHp;

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        currentHp = maxHp;
    }

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
        Destroy(gameObject);
    }
}
