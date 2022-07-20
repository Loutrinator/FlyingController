using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform; 
    [SerializeField] private Vector2 canvasRenderSize; 
    [SerializeField] private Camera mainCam; 
    [SerializeField] private float radius = 80f;
    [SerializeField] private float lerpSpeed = 10f;

    private Vector3 currentPos;
    private Vector3 mouseWorldPos;
    private Vector3 target;

    public Vector3 GetTarget()
    {
        return target;
    }
    private void Start()
    {
        UnityEngine.Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 screenPos = new Vector3(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height,10);
        
        currentPos = Vector3.Lerp(currentPos, screenPos, Time.deltaTime * lerpSpeed);
        Vector2 pixelPos = (currentPos - new Vector3(0.5f,0.5f,0f))*canvasRenderSize;
        float magnitude = pixelPos.magnitude < radius ? pixelPos.magnitude : radius;
        Vector2 newPos = pixelPos.normalized* magnitude;
        Debug.Log("(" + pixelPos.x + " " + pixelPos.y + ")");
        rectTransform.anchoredPosition = new Vector2((int)newPos.x,(int)newPos.y);
    }

    private void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mouseWorldPos = mainCam.ScreenToWorldPoint(mousePos);
        Vector3 dir = mouseWorldPos - mainCam.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(mainCam.transform.position, dir, out hit, 1000f))
        {
            target = hit.point;
        }
        else
        {
            target = dir.normalized * 1000f + mainCam.transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(mainCam.transform.position,mouseWorldPos);
    }
}
