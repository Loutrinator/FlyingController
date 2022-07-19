using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float timeAlive = 5f;

    private MeshRenderer laserRenderer;
    private float startTime;
    
    private bool disabled = false;

    private void Start()
    {
        laserRenderer = GetComponent<MeshRenderer>();
        Reset();
    }

    public void Reset()
    {
        startTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (disabled) return;
        transform.position += transform.forward * (speed * Time.fixedDeltaTime);
        if (Time.time - startTime > timeAlive)
        {
            Disable();
        }
    }

    private void Disable()
    {
        disabled = true;
        laserRenderer.enabled = false;
    }
}
