using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioParser : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    private static float[] _samples = new float[512];
    private static float[] _freqBand = new float[8];

    public static float[] GetSamples()
    {
        return _samples;
    }
    public static float GetFrequencyBand(int pos)
    {
        return _freqBand[pos];
    }
    // Start is called before the first frame update
    void Start()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
    }

    private void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples,0,FFTWindow.Blackman);
    }

    private void MakeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7) sampleCount += 2;

            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;
            _freqBand[i] = average * 10;
        }

    }
}
