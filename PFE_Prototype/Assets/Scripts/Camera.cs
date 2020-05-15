using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [Header("Camera Parameters")]
    public Transform targetFocus;

    public float distance = 5.0f;

    public float verticalOffset = 1.0f;
    public float horizontalOffset = 2.0f;

   
    public Vector3 rotationCamera;

    // Update is called once per frame
    void Update()
    {
        Vector3 vOffset = Vector3.up * verticalOffset;
        Vector3 hOffset = Vector3.right * horizontalOffset;
        Vector3 dist = Vector3.forward * -distance;

        transform.position = targetFocus.position + dist + vOffset + hOffset;
        transform.rotation = Quaternion.Euler(rotationCamera);
    }
}
