using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracking : MonoBehaviour
{
    public Transform target;
    public bool modifyX;
    public bool modifyY;
    public bool modifyZ;

    private void LateUpdate()
    {
        float x = modifyX ? target.position.x: transform.position.x;
        float y = modifyY ? target.position.y: transform.position.y;
        float z = modifyZ ? target.position.z: transform.position.z;
        transform.position =  new Vector3(x, y, z);
    }
}
