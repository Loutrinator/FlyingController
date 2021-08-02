using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class MoveBetch : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float boost;
    [SerializeField] private float boostLerpSpeed;
    [SerializeField] private float rollIntensity;
    [SerializeField] private float rollSpeed;
    [SerializeField] private float pitchIntensity;
    [SerializeField] private float pitchSpeed;
    private Vector2 _movement; 
    public bool boostOn;
    private float _boostForce;
    private Vector3 _velocity;

    private void Awake()
    {
        _movement = Vector2.zero;
        _velocity = Vector3.zero;
    }

    private void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        boostOn = Input.GetKey(KeyCode.LeftShift);
    }

    private void FixedUpdate()
    {
        float pitch = _movement.y;
        
        _boostForce = Mathf.Lerp(_boostForce, boostOn ? boost : 0, boostLerpSpeed);
        transform.position += transform.forward * ((_boostForce+speed)*Time.fixedDeltaTime);
        Vector3 eulers = transform.eulerAngles;
        float rollAngle = Mathf.Lerp(eulers.z, -_movement.x * rollIntensity, rollSpeed);
        float pitchAngle = Mathf.Lerp(eulers.z, _movement.y * pitchIntensity, pitchSpeed);
        
        transform.RotateAround(transform.forward, rollAngle * Time.fixedDeltaTime);
        transform.RotateAround(transform.right, pitchAngle * Time.fixedDeltaTime);
    }
}
