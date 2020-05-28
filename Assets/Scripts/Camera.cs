using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [Header("Camera Parameters")]
    public Transform targetFocus;

    public float distance = 5.0f;

    private float _verticalOffset = 0f;
    public float horizontalOffset = 2.0f;
    public float constantHeight = 15f;
    public float lerpTime = 0.1f;
   
    public Vector3 rotationCamera;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 vOffset = Vector3.up * _verticalOffset;
        Vector3 hOffset = Vector3.right * horizontalOffset;
        Vector3 dist = Vector3.forward * -distance;
        Vector3 finalPos = targetFocus.position + dist + vOffset + hOffset;
        finalPos.y =constantHeight;

        //transform.position = finalPos;
        transform.position = Vector3.Lerp(transform.position, finalPos, lerpTime);
        transform.rotation = Quaternion.Euler(rotationCamera);


        
    }
}
