using System;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public enum MusicTransformType
{
 Scale,
 Position
}
[RequireComponent(typeof(AudioLoudness))]
public class MusicTransform : MonoBehaviour
{

    [SerializeField] public MusicTransformType type;
    [SerializeField] private float intensity = 1f;
    [SerializeField] private float minTransformation = 0f;
    [SerializeField] private float maxTransformation = 1f;
    public bool modifyX;
    public bool modifyY;
    public bool modifyZ;
    private AudioLoudness _audioLoudness;
    private Vector3 baseValue;
    

    private void Start()
    {
        _audioLoudness = gameObject.GetComponent<AudioLoudness>();
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
        float loudness = _audioLoudness.clipLoudness;
        loudness *= intensity;
        loudness = Mathf.Clamp(loudness, minTransformation, maxTransformation);
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