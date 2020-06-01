using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    public static bool isJumping = false;
    [SerializeField]
    private float _playerJumpForce = 5f;
    [SerializeField]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float stopJumpTimer;
    public float timeInTheAir;
    Rigidbody rb;

    [Header("RayCast")]
    public GameObject[] downObj;
    public float rayDownLength;


    public GROUND_STATE groundState = GROUND_STATE.GROUNDED;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {


        if (!CheckGroundStatus())
        {
            isJumping = true;

        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton0) && (!isJumping || CheckGroundStatus()))
        {


            Player.playerInstance.moveVectorJump = Player.playerInstance.moveVector;
            Debug.Log("jump");
            rb.velocity = new Vector3(0f, _playerJumpForce, 0f);
            isJumping = true;
            
        }
        else if (CheckGroundStatus() && timeInTheAir > Time.deltaTime)
        {
            //_CanJump = true;
            isJumping = false;
        }




        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.JoystickButton0))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        
        if (isJumping)
        {
            timeInTheAir += Time.deltaTime;
        }
        else if (!isJumping)
        {
            timeInTheAir = 0;

        }


        if (timeInTheAir >= stopJumpTimer)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }



    bool CheckGroundStatus()
    {
        
        foreach (GameObject obj in downObj)
        {
            Ray downRay = new Ray(obj.transform.position, Vector3.down * rayDownLength);
            Debug.DrawRay(obj.transform.position, Vector3.down * rayDownLength, Color.red);

            if (Physics.Raycast(downRay, rayDownLength))
            {
                return true;
            }

        }

        return false;
    }

}

   

public enum GROUND_STATE
{
    GROUNDED,
    AIRBORNE
}