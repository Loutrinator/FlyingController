using System;
using UnityEngine;

public class DestroyOnParticleEnd : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps; 
    private void Update()
    {
        if(!ps.IsAlive()) Destroy(gameObject);
    }
}
