using System;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public enum MusicTransformType
{
 Scale,
 Position
}
public class MusicTransform : MonoBehaviour
{

    [SerializeField] public MusicTransformType type;
    [SerializeField] private int band = 0;
    [SerializeField] private float lerpSpeed = 0.2f;
    [SerializeField] private float intensity = 1f;
    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 1f;
    public bool modifyX;
    public bool modifyY;
    public bool modifyZ;
    private Vector3 baseValue;

    private float loudness = 0f;
    

    private void Start()
    {
        switch (type)
        {
            case MusicTransformType.Position:
                baseValue = transform.position;
                break;
            case MusicTransformType.Scale:
                baseValue = transform.localScale;
                break;
        }
    }

    public void Update()
    {
        loudness = Mathf.Lerp(loudness, AudioParser.GetFrequencyBand(band),lerpSpeed * Time.deltaTime);
        loudness = Mathf.Clamp(loudness * intensity, minValue, maxValue);
        float x = modifyX ? baseValue.x * loudness: baseValue.x;
        float y = modifyY ? baseValue.y * loudness: baseValue.y;
        float z = modifyZ ? baseValue.z * loudness: baseValue.z;
        Vector3 newVal = new Vector3(x, y, z);
        if (type == MusicTransformType.Position)
        {
            transform.position = newVal;
        } else if (type == MusicTransformType.Scale)
        {
            transform.localScale = newVal;
        }
    }
}