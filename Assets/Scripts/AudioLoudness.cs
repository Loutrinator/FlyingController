using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoudness : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    public float updateStep = 0.05f;
    public int sampleDataLength = 1024;

    private float currentUpdateTime = 0f;

    public float clipLoudness;

    private float[] clipSampleData;

    private void Awake()
    {
        clipSampleData = new float[sampleDataLength];

    }

    private void Update()
    {
        currentUpdateTime += 0f;
        source.clip.GetData(clipSampleData, source.timeSamples);
        clipLoudness = 0f;
        foreach (var sample in clipSampleData)
        {
            clipLoudness += Mathf.Abs(sample);
        }

        clipLoudness /= sampleDataLength;
    }
}
