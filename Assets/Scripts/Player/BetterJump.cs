using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void LegAction();


public class BetterJump : MonoBehaviour
{
    public LegAction legAction;

    [Header("Pomme")]
    public Renderer legs;

    [Header("Jump Movements")]
    public static bool isJumping = false;
    [SerializeField]
    private float _playerJumpForce = 5f;
    [SerializeField]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float stopJumpTimer;
    public float timeInTheAir;
    public float jumpCount = 0f;
    public bool jumpAbility = false;
    Rigidbody rb;

    [Header("RayCast")]
    public GameObject[] downObj;
    public float rayDownLength;

    [Header("Legs Abilities")]
    public int legCount = 0;
    public LEGTYPE legType = LEGTYPE.DEFAULT;

    [Header("BearLegs")]
    public float bearSpeed = 8.1f;
    public float bearFall;
    [Header("EagleLegs")]
    public float eagleSpeed = 8.1f;
    public Transform eggFirePoint;
    public GameObject eggPrefab;
    [Header("LynxLegs")]
    public float lynxDash;
    public float lynxJump;
    public float lynxSpeed = 10f;




    //public GROUND_STATE groundState = GROUND_STATE.GROUNDED;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        

        float rightTrigger = Input.GetAxisRaw("RightTrigger");
        float leftTrigger = Input.GetAxisRaw("LeftTrigger");

        #region LegCount

        if (Input.GetKeyDown(KeyCode.JoystickButton4))
        {
            legCount--;
            if (legCount < 0)
            {
                legCount = 3;
            }

            // Compte les legs :D
            legType = GetLegTypeFromIndex(legCount);
            SetLegAction(legType);
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton5))
        {
            legCount++;
            if (legCount >= 4)
            {
                legCount = 0;
            }
            // Compte les legs :D
            legType = GetLegTypeFromIndex(legCount);
            SetLegAction(legType);
        }

        
       
        #endregion

        

        //Logique pour la compétence des jammbes
        if (Input.GetKeyDown(KeyCode.JoystickButton0) && jumpAbility && jumpCount == 1f)
        {
            timeInTheAir = 0f;
            isJumping = true;
            jumpCount = 0f;
            Player.playerInstance.moveVectorJump = Player.playerInstance.moveVector;
            legAction();
        

        }

        // Jump system checking the ground status. If true you can jump with velocity, if false, you are juming
        if (!CheckGroundStatus())
        {
            isJumping = true;

        }
        else if (Input.GetKeyDown(KeyCode.JoystickButton0) && (!isJumping || CheckGroundStatus()) && Player.playerInstance.playerDead == false)
        {


            Player.playerInstance.moveVectorJump = Player.playerInstance.moveVector;
            Debug.Log("jump");
            rb.velocity = new Vector3(0f, _playerJumpForce, 0f);
            isJumping = true;
            jumpCount = 1f;
            
        }
        else if (CheckGroundStatus() && timeInTheAir > Time.deltaTime)
        {
            
            isJumping = false;
            
        }  
        
        
        


        // logique pour avoir deux jumps différents
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        else if (rb.velocity.y > 0 /*&& !Input.GetKey(KeyCode.JoystickButton0)*/)  
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // logique afin de limiter le temps dans les airs
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


    // un set de raycast qui check si le joueur touche le sol ou non
    bool CheckGroundStatus()
    {
        
        foreach (GameObject obj in downObj)
        {
            Ray downRay = new Ray(obj.transform.position, Vector3.down * rayDownLength);
            Debug.DrawRay(obj.transform.position, Vector3.down * rayDownLength, Color.cyan);

            if (Physics.Raycast(downRay, rayDownLength))
            {
                return true;
            }

        }

        return false;
    }

    LEGTYPE GetLegTypeFromIndex (int i)
    {
        LEGTYPE lt = LEGTYPE.DEFAULT;
        lt = (LEGTYPE)i;
        return lt;
    }


    void DefaultLegs()
    {
        // tu ne fais rien. tu ne sers à rien. Tu es inutile. 
        
    }
    void BearLegs()
    {

        Debug.Log("BearJump");
        rb.velocity = new Vector3(0f, -bearFall, 0f);
        
    }
    
    void EagleLegs()
    {
        Debug.Log("EagleJump");
        rb.velocity = new Vector3(0f, _playerJumpForce, 0f);
        Instantiate(eggPrefab, eggFirePoint.position, eggFirePoint.rotation);
    }

    void LynxLegs ()
    {
        Debug.Log("LynxJump");
        rb.velocity = new Vector3(Player.playerInstance.horizontalMovement * lynxDash, lynxJump, Player.playerInstance.verticalMovement * lynxDash) ;
    }

    void SetLegAction(LEGTYPE lt)
    {
        switch (lt)
        {
            case LEGTYPE.DEFAULT:
                legAction = DefaultLegs;
                legs.GetComponent<Renderer>().material.color = Color.green;
                Player.playerInstance.playerMaxSpeed = 8f;
                Player.playerInstance._playerSpeed = 8f;
                break;
            case LEGTYPE.BEAR:
                legAction = BearLegs;
                legs.GetComponent<Renderer>().material.color = Color.black;
                Player.playerInstance.playerMaxSpeed = bearSpeed;
                Player.playerInstance._playerSpeed = bearSpeed;
                break;
            case LEGTYPE.EAGLE:
                legAction = EagleLegs;
                legs.GetComponent<Renderer>().material.color = Color.white;
                Player.playerInstance.playerMaxSpeed = eagleSpeed;
                Player.playerInstance._playerSpeed = eagleSpeed;
                break;
            case LEGTYPE.LYNX:
                legAction = LynxLegs;
                legs.GetComponent<Renderer>().material.color = Color.yellow;
                Player.playerInstance.playerMaxSpeed = lynxSpeed;
                Player.playerInstance._playerSpeed = lynxSpeed;
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            jumpCount = 0f;

        }
    }
}

 public enum LEGTYPE
 {
    DEFAULT,
    BEAR,
    EAGLE,
    LYNX
 }  

public enum GROUND_STATE
{
    GROUNDED,
    AIRBORNE
}