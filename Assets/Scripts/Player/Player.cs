using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player playerInstance;
        
    [Header("Mouvements")]
    [SerializeField]
    private float _playerSpeed = 5f;
    [SerializeField]
    public float playerMaxSpeed = 20f;
    [SerializeField]
    private float _playerMinSpeed = 5f;
    [SerializeField]
    private float _playerSpeedIncr = 0.90f;
    [SerializeField]
    private float _playerSpeedDecr = 0.90f;
    [SerializeField]
    private float _airControlRatio = 0.5f;
    [SerializeField]
    public float dashVelocity = 10f;
    public Vector3 moveVector;
    public Vector3 moveVectorJump;
    public Vector3 playerDirection;
    public float horizontalMovement;
    public float verticalMovement;
    private Vector3 lastDir;

    [Header("Bools")]
    [SerializeField]
    private bool _CanEagleAttack = true;
    [SerializeField]
    private bool _canNewBearAttack = true;
    [SerializeField]
    private bool _canNewEagleAttack = true;
    [SerializeField]
    private bool _canLynxAttack = true;
    [SerializeField]
    private bool _canMove = true;
    [SerializeField]
    private bool _canRotate = true;


    [Header("PlayerInfo")]
    public Rigidbody rb;
    public GameObject render;
    public Animator animator;
    public Renderer arms;

    [Header("Attack")]
    [SerializeField]
    private Vector3 _bearAttackRange;
    public LayerMask enemyLayer;
    public float attackResetTimer = 2f;
    public float eagleTime = 0f;
    public int attackNum = 0;
    public float channelingBasicAttack = 0f;
    public Transform basicTransform;







    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        playerInstance = this;
        //basicCollider = GetComponentInChildren<EnemyDetectorBasic>();
    }

    // Update is called once per frame
    void Update()
    {



        // Identification des mouvements
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");


        moveVector = new Vector3(horizontalMovement, 0f, verticalMovement);

        if (BetterJump.isJumping)
        {
            rb.MovePosition(rb.position + (moveVectorJump * _playerSpeed) * Time.deltaTime);
            moveVectorJump += moveVector * _airControlRatio * Time.deltaTime;
            moveVectorJump = Vector3.ClampMagnitude(moveVectorJump, 1f);
        }
        else
        {
            if (_canMove)
            {
                rb.MovePosition(rb.position + moveVector * _playerSpeed * Time.deltaTime);

            }
            else
            {
                rb.MovePosition(rb.position + Vector3.zero * Time.deltaTime);
            }
            // ce qui fait bouger le personnages
        }


        //direction du personnage : on fait rotate le render et non le player en tant que tel

        if ((horizontalMovement != 0 || verticalMovement != 0) && _canRotate)
        {
            render.transform.rotation = Quaternion.LookRotation(moveVector);

            if (horizontalMovement < 0)
            {
                playerDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f).normalized;

            }
            if (horizontalMovement > 0)
            {
                playerDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f).normalized;

            }
            if (verticalMovement < 0)
            {
                playerDirection = new Vector3(0f, 0f, Input.GetAxis("Vertical")).normalized;

            }
            if (verticalMovement > 0)
            {
                playerDirection = new Vector3(0f, 0f, Input.GetAxis("Vertical")).normalized;

            }

            if (verticalMovement > 0 && horizontalMovement > 0)
            {
                playerDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

            }
            if (verticalMovement > 0 && horizontalMovement < 0)
            {
                playerDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

            }
            if (verticalMovement < 0 && horizontalMovement > 0)
            {
                playerDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

            }
            if (verticalMovement < 0 && horizontalMovement < 0)
            {
                playerDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

            }
        }


        // speed Incr
        if (horizontalMovement > 0)
        {
            if (_playerSpeed < playerMaxSpeed)
            {
                _playerSpeed += (Time.deltaTime * _playerSpeedIncr);
                if (_playerSpeed > playerMaxSpeed)
                {
                    _playerSpeed = playerMaxSpeed;
                }
            }

        }

        if (horizontalMovement < 0)
        {
            if (_playerSpeed < playerMaxSpeed)
            {
                _playerSpeed += (Time.deltaTime * _playerSpeedIncr);
                if (_playerSpeed > playerMaxSpeed)
                {
                    _playerSpeed = playerMaxSpeed;
                }
            }

        }


        if (verticalMovement < 0)
        {
            if (_playerSpeed < playerMaxSpeed)
            {
                _playerSpeed += (Time.deltaTime * _playerSpeedIncr);
                if (_playerSpeed > playerMaxSpeed)
                {
                    _playerSpeed = playerMaxSpeed;
                }
            }

        }

        if (verticalMovement > 0)
        {
            if (_playerSpeed < playerMaxSpeed)
            {
                _playerSpeed += (Time.deltaTime * _playerSpeedIncr);
                if (_playerSpeed > playerMaxSpeed)
                {
                    _playerSpeed = playerMaxSpeed;
                }
            }

        }
        // speed decrease
        if (horizontalMovement == 0 && verticalMovement == 0)
        {
            _playerSpeed *= _playerSpeedDecr;   // speedDecr entre 0 et 1
            if (_playerSpeed < _playerMinSpeed)
            {
                _playerSpeed = _playerMinSpeed;
            }
        }




        // LynxAttack
        if (Input.GetKeyDown(KeyCode.Joystick1Button2) && _canLynxAttack == true)
        {


            StartCoroutine(LynxAttack());

            // Eagle Attack
            /* if (_CanEagleAttack == true && attackNum == 0)
             {


                 StartCoroutine("EagleAnime1");
                 StartCoroutine("ResetTime");
                 attackNum += 1;
                 Debug.Log("Eagle1");


             }

             if (_CanEagleAttack == true && attackNum == 1)
             {

                 StopCoroutine("ResetTime");

                 StartCoroutine("EagleAnime2");
                 StartCoroutine("ResetTime");
                 attackNum += 1;
                 Debug.Log("Eagle2");


             }
             if (_CanEagleAttack == true && attackNum == 2)
             {

                 //StartCoroutine("EagleAttack3");
                 StartCoroutine("EagleAnime3");
                 attackNum = 0;
                 Debug.Log("Eagle3");

             }*/

        }


        //NewBear Attack
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            if (_canNewBearAttack)
            {
                StartCoroutine("NewBearAttack");
            }
        }

        //NewEagle Attack
        /*if (Input.GetKeyDown(KeyCode.Joystick1Button1) && _canNewEagleAttack)
        {

            StartCoroutine(NewEagleAttackRoutine());
        }*/

        if (Input.GetKey(KeyCode.Joystick1Button1))
        {

            BasicAttackRange();



        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button1))
        {
            BasicAttack();
            channelingBasicAttack = 0f;
            basicTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    // Not use for now
    void Attack ()
    {
        

        foreach (Enemy enemy in EnemyDectectorSphere.EnemiesDetectedSphere)
        {
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(20);
            enemy.GetComponent<Enemy>().Knockback(100);

        }
    }

    
    // Not use for now
    void GrizzlyAttack()
    {
        foreach (Enemy enemy in EnemyDetectorRectangle.EnemiesDetectedRectangle)
        {
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(40);
            enemy.GetComponent<Enemy>().Knockback(200);

        }
    }

    // Not use for now
    IEnumerator TigerAttack()
    {
        int l = EnemyDetectorRectangle.EnemiesDetectedRectangle.Count;
        Debug.Log("TigerAttack");

        for (int i = l - 1; i >= 0; i--)
        {
            Enemy enemy = EnemyDetectorRectangle.EnemiesDetectedRectangle[i];
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(20);
            enemy.Knockback(50);
            
        }
        yield return new WaitForSeconds(0.2f);

        
        l = EnemyDetectorRectangle.EnemiesDetectedRectangle.Count;
        for (int i= l-1; i >=0 ; i--)
        {
            Enemy enemy = EnemyDetectorRectangle.EnemiesDetectedRectangle[i];
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(20);
            enemy.Knockback(50);
        }
    }

    // Not use for now
    void EagleAttack1()
    {
        
        foreach (Enemy enemy in EnemyDectectorSphere.EnemiesDetectedSphere)
        {
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(10);
            enemy.GetComponent<Enemy>().Knockback(100);

        }
        

    }
    // Not use for now
    IEnumerator EagleAnime1()
    {
        _CanEagleAttack = false;
        animator.SetBool("EagleAttackDone1", false);
        yield return new WaitForSeconds(0.2f);
        _CanEagleAttack = true;
        
        animator.SetBool("EagleAttackDone1", true);
    }
    // Not use for now
    void EagleAttack2()
    {
        
        foreach (Enemy enemy in EnemyDectectorSphere.EnemiesDetectedSphere)
        {
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(30);
            enemy.GetComponent<Enemy>().Knockback(100);

        }
      
    }
    // Not use for now
    IEnumerator EagleAnime2()
    {
        _CanEagleAttack = false;
        animator.SetBool("EagleAttackDone2", false);
        yield return new WaitForSeconds(0.2f);
        _CanEagleAttack = true;
        
        animator.SetBool("EagleAttackDone2", true);
    }
    // Not use for now
    void EagleAttack3()
    {
       
        foreach (Enemy enemy in EnemyDectectorSphere.EnemiesDetectedSphere)
        {
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(60);
            enemy.GetComponent<Enemy>().Knockback(100);

        }
       
    }
    // Not use for now
    IEnumerator EagleAnime3()
    {
        _CanEagleAttack = false;
        animator.SetBool("EagleAttackDone3", false);
        yield return new WaitForSeconds(0.2f);
        _CanEagleAttack = true;
        
        animator.SetBool("EagleAttackDone3", true);
    }
    // Not use for now
    IEnumerator ResetTime()
    {
        eagleTime = 0f;
        while (eagleTime < attackResetTimer)
        {
            eagleTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            Debug.Log(eagleTime);

        }

        if (eagleTime >= 1)
        {
            attackNum = 0;
            eagleTime = 0f;
            Debug.Log("EagleReset");
        }
    }
    
    IEnumerator NewBearAttack()
    {
        int l = EnemyDetectorBear.EnemiesDetectedBear.Count;
        _canNewBearAttack = false;
        _canMove = false;
        _canRotate = false;

        for (int i = l - 1; i >= 0; i--)
        {
            Enemy enemy = EnemyDetectorBear.EnemiesDetectedBear[i];
            Debug.Log("NewBearAttack!");
            enemy.TakeDamamge(50);
            enemy.GetComponent<Enemy>().Knockback(200);

        }
        

        yield return new WaitForSeconds(0.5f);
        _canNewBearAttack = true;
        _canMove = true;
        _canRotate = true;


    }
    // Not use for now
    void NewEagleAttack()
    {
        int l = EnemyDetectorEagle.EnemiesDetectedEagle.Count;

        for (int i = l - 1; i >= 0; i--)
        {
            Enemy enemy = EnemyDetectorBear.EnemiesDetectedBear[i];
            Debug.Log("NewBearAttack!");
            enemy.TakeDamamge(10);
            enemy.GetComponent<Enemy>().Knockback(100);

        }
    }
    // Not use for now
    IEnumerator AnimNewEagleAttack()
    {
        _canNewEagleAttack = false;
        animator.SetBool("NewEagle", false);
        yield return new WaitForSeconds(2f);
        animator.SetBool("NewEagle", true);
        _canNewEagleAttack = true;

    }

    IEnumerator NewEagleAttackRoutine()
    {
        int l = EnemyDetectorEagle.EnemiesDetectedEagle.Count;
        _canNewEagleAttack = false;
        for (int i = l - 1; i >= 0; i--)
        {
            Enemy enemy = EnemyDetectorEagle.EnemiesDetectedEagle[i];
            Debug.Log("EagleAttack!");
            enemy.TakeDamamge(10);
            enemy.GetComponent<Enemy>().Knockback(100);

        }
        yield return new WaitForSeconds(2f);
        _canNewEagleAttack = true;
    }

    IEnumerator LynxAttack()
    {
        
        _canLynxAttack = false;


        Dash(10);


        yield return new WaitForSeconds(0.4f);

        _canLynxAttack = true;
        int l = EnemyDetectorLynx.EnemiesDetectedLynx.Count;

        for (int i = l - 1; i >= 0; i--)
        {
            Enemy enemy = EnemyDetectorLynx.EnemiesDetectedLynx[i];
            Debug.Log("LynxAttack!");
            enemy.TakeDamamge(50);
            enemy.GetComponent<Enemy>().Knockback(200);
        }
        


    }

    void BasicAttackRange()
    {
        _canRotate = false;

        if (channelingBasicAttack >= 3)
        {
            channelingBasicAttack = 3f;
        }
        else channelingBasicAttack += Time.deltaTime;


        if (channelingBasicAttack >= 0  && channelingBasicAttack < 1)
        {
            basicTransform.localScale = new Vector3(1, 1 , 1 );
        }
        else if (channelingBasicAttack >= 1 && channelingBasicAttack < 2)
        {
            basicTransform.localScale = new Vector3(1, 1, 2);
        }
        else if (channelingBasicAttack >= 2 && channelingBasicAttack <= 3)
        {
            basicTransform.localScale = new Vector3(1, 1, 3);
        }
        else basicTransform.localScale = new Vector3(1, 1, 1);

        
    }

    void BasicAttack()
    {
        _canRotate = true;
        Debug.Log("BasicAttack");
        int l = EnemyDetectorBasic.EnemiesDetectedBasic.Count;


        for (int i = l - 1; i >= 0; i--)
        {
            Enemy enemy = EnemyDetectorBasic.EnemiesDetectedBasic[i];
            Debug.Log("You hit Enemies");


            if (channelingBasicAttack >= 0 && channelingBasicAttack < 1)
            {
                enemy.TakeDamamge(15);
                enemy.Knockback(15);
            }
            else if (channelingBasicAttack >= 1 && channelingBasicAttack < 2)
            {
                enemy.TakeDamamge(30);
                enemy.Knockback(30);
            }
            else if (channelingBasicAttack >= 2 && channelingBasicAttack <= 3)
            {
                enemy.TakeDamamge(50);
                enemy.Knockback(200);
            }
           
            

        }
    }


    void Dash(float dashPower)
    {
        
        
        if (verticalMovement ==0 && horizontalMovement == 0)
        {
            rb.velocity = playerDirection * dashPower;
        }
        else rb.velocity = new Vector3(horizontalMovement, 0f, verticalMovement) * dashPower;
    }
}

