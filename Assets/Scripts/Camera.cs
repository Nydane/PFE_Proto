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

    private void Start()
    {
        
    }

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


         /*if (targetFocus.transform.position.y >= 7.5)
         {
             constantHeight = 20f;
         }
         else constantHeight = 15f;*/

        if (!BetterJump.isJumping)
        {
            constantHeight = GetCameraheight(15, 1.9f, 5.5f, 5f, targetFocus.transform.position.y);

        }


    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="baseCamHeight">constant Height : hauteur minimum de la camera</param>
    /// <param name="baseHeight">hauteur minimum du player</param>
    /// <param name="deltaX">monter en Y que doit prendre le joueur avant que la camera bouge</param>
    /// <param name="deltaY">réaction de la caméra a chaque fois qu'une step est passé</param>
    /// <param name="y">hauteur actuelle du joueur</param>
    /// <returns></returns>
    float GetCameraheight(float baseCamHeight, float baseHeight, float deltaX, float deltaY, float y)
    {
        int i = 0;
        float posY = y;
        while(posY - deltaX >= baseHeight)
        {
            i++;
            posY -= deltaX;
                
        }

        return baseCamHeight + i * deltaY;
    }
    
}
