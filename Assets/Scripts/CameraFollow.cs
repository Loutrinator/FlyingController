using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    private ShipController target;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float rotateLerpSpeed = 1f;
    [SerializeField]
    private float FOVBoost = 1f;
    [SerializeField]
    private float ZBoost = 1f;
    [SerializeField]
    private float BoostEffectSpeed = 1f;

    private float boostEffect;

    private Vector3 camStartPos;
    private float startFOV;
    private void Start()
    {
        camStartPos = cam.transform.localPosition;
        startFOV = cam.fieldOfView;
    }

    void FixedUpdate()
    {
        transform.position = target.transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, Time.fixedDeltaTime * rotateLerpSpeed);
        Vector3 newCamPos = camStartPos;

        int boostOn = target.boostOn ? 1 : 0;
        boostEffect = Mathf.Lerp(boostEffect, 1 * boostOn, BoostEffectSpeed * Time.fixedDeltaTime);
        newCamPos.z += ZBoost * boostEffect;

        cam.transform.localPosition = newCamPos;
        cam.fieldOfView = startFOV +  FOVBoost * boostEffect;

    }
}
