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
    [SerializeField] private Laser laserPrefab;
    [SerializeField] private float shootingFrequency = 0.3f;
    [SerializeField] private List<Transform> canonPosition;
    private Vector3 _movement; 
    public bool boostOn;
    private float _boostForce;
    private float elapsedBetweenFire;

    private void Awake()
    {
        _movement = Vector2.zero;
    }

    private void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");
        _movement.z = Input.GetAxis("Rotate");
        boostOn = Input.GetKey(KeyCode.LeftShift);
        if(Input.GetAxis("Fire") > 0) Shoot();
    }

    private void FixedUpdate()
    {
        float pitch = _movement.y;
        
        _boostForce = Mathf.Lerp(_boostForce, boostOn ? boost : 0, boostLerpSpeed);
        transform.position += transform.forward * ((_boostForce+speed)*Time.fixedDeltaTime);
        Vector3 eulers = transform.eulerAngles;
        float yawAngle = Mathf.Lerp(eulers.z, _movement.x * rollIntensity, rollSpeed);
        float pitchAngle = Mathf.Lerp(eulers.z, _movement.y * pitchIntensity, pitchSpeed);
        float rollAngle = Mathf.Lerp(eulers.z, -_movement.z * pitchIntensity, pitchSpeed);
        
        transform.RotateAround(transform.forward, rollAngle * Time.fixedDeltaTime);
        transform.RotateAround(transform.up, yawAngle * Time.fixedDeltaTime);
        transform.RotateAround(transform.right, pitchAngle * Time.fixedDeltaTime);
    }

    private void Shoot()
    {
        elapsedBetweenFire += Time.deltaTime;
        if (elapsedBetweenFire > shootingFrequency)
        {
            elapsedBetweenFire -= shootingFrequency;
            foreach (var canon in canonPosition)
            {
                Instantiate(laserPrefab, canon.position + canon.forward*1f, canon.rotation);
            }
        }
    }
}
