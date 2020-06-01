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
    private float _playerMaxSpeed = 20f;
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
    private float horizontalMovement;
    private float verticalMovement;
    

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

    [Header("Attack")]
    public Transform attackPoint;
    public Transform bearAttackPoint;
    [SerializeField]
    private float _attackRange = 5f;
    [SerializeField]
    private Vector3 _bearAttackRange;
    public LayerMask enemyLayer;
    public float attackResetTimer = 2f;
    public float eagleTime = 0f;
    public int attackNum = 0;
    







    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        playerInstance = this;
    }

    // Update is called once per frame
    void Update()
    {

        
        
        // Identification des mouvements
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        //Debug.Log(horizontalMovement);
        //Debug.Log(verticalMovement);

        moveVector = new Vector3(horizontalMovement, 0f, verticalMovement);

        if (BetterJump.isJumping)
        {
            rb.MovePosition(rb.position + (moveVectorJump * _playerSpeed )  * Time.deltaTime);
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

        Vector3 playerDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if ((horizontalMovement != 0 || verticalMovement !=0) && _canRotate)
        {
            render.transform.rotation = Quaternion.LookRotation(playerDirection);
        }
        
       

        // speed Incr
        if (horizontalMovement > 0)
        {
            if (_playerSpeed < _playerMaxSpeed)
            {
                _playerSpeed += (Time.deltaTime * _playerSpeedIncr);
                if (_playerSpeed > _playerMaxSpeed)
                {
                    _playerSpeed = _playerMaxSpeed;
                }
            }

        }

        if (horizontalMovement < 0)
        {
            if (_playerSpeed < _playerMaxSpeed)
            {
                _playerSpeed += (Time.deltaTime * _playerSpeedIncr);
                if (_playerSpeed > _playerMaxSpeed)
                {
                    _playerSpeed = _playerMaxSpeed;
                }
            }

        }
        

        if (verticalMovement < 0)
        {
            if (_playerSpeed < _playerMaxSpeed)
            {
                _playerSpeed += (Time.deltaTime * _playerSpeedIncr);
                if (_playerSpeed > _playerMaxSpeed)
                {
                    _playerSpeed = _playerMaxSpeed;
                }
            }

        }

        if (verticalMovement > 0)
        {
            if (_playerSpeed < _playerMaxSpeed)
            {
                _playerSpeed += (Time.deltaTime * _playerSpeedIncr);
                if (_playerSpeed > _playerMaxSpeed)
                {
                    _playerSpeed = _playerMaxSpeed;
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
        if (Input.GetKeyDown(KeyCode.Joystick1Button1)&&_canNewEagleAttack)
        {
           
            StartCoroutine("AnimNewEagleAttack");
        }
    }

    
    void Attack ()
    {
        /* Collider[] hitEnemies =  Physics.OverlapSphere(attackPoint.position, _attackRange, enemyLayer);

          foreach (Collider enemy in hitEnemies)
          {
              Debug.Log("You hit Enemies");
              enemy.GetComponent<Enemy>().TakeDamamge(20);
              enemy.GetComponent<Enemy>().Knockback(50);

          }*/

        foreach (Enemy enemy in EnemyDectectorSphere.EnemiesDetectedSphere)
        {
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(20);
            enemy.GetComponent<Enemy>().Knockback(100);

        }
    }

    // même attaque que le grizzly mais faites d'une seconde façon
    void BearAttack()
    {


        Collider[] hitEnemies = Physics.OverlapBox(bearAttackPoint.position, _bearAttackRange,render.transform.rotation, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("You hit Enemies");
            enemy.GetComponent<Enemy>().TakeDamamge(40);
            enemy.GetComponent<Enemy>().Knockback(200);
        }
    }
    

    void GrizzlyAttack()
    {
        foreach (Enemy enemy in EnemyDetectorRectangle.EnemiesDetectedRectangle)
        {
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(40);
            enemy.GetComponent<Enemy>().Knockback(200);

        }
    }


    IEnumerator TigerAttack()
    {
        int l = EnemyDetectorRectangle.EnemiesDetectedRectangle.Count;
       /* foreach (Enemy enemy in Detector.EnemiesDetected)
        {
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(20);
            enemy.Knockback(50);
            
        }*/

        for (int i = l - 1; i >= 0; i--)
        {
            Enemy enemy = EnemyDetectorRectangle.EnemiesDetectedRectangle[i];
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(20);
            enemy.Knockback(50);
            
        }
        yield return new WaitForSeconds(0.2f);

        /*foreach(Enemy enemy in Detector.EnemiesDetected)
        {
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(20);
            enemy.Knockback(50);

        }*/
        l = EnemyDetectorRectangle.EnemiesDetectedRectangle.Count;
        for (int i= l-1; i >=0 ; i--)
        {
            Enemy enemy = EnemyDetectorRectangle.EnemiesDetectedRectangle[i];
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(20);
            enemy.Knockback(50);
        }
    } 
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, _attackRange);
        Gizmos.DrawWireCube(bearAttackPoint.position, _bearAttackRange);
        
        
    }*/

    void EagleAttack1()
    {
        
        foreach (Enemy enemy in EnemyDectectorSphere.EnemiesDetectedSphere)
        {
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(10);
            enemy.GetComponent<Enemy>().Knockback(100);

        }
        

    }

    IEnumerator EagleAnime1()
    {
        _CanEagleAttack = false;
        animator.SetBool("EagleAttackDone1", false);
        yield return new WaitForSeconds(0.2f);
        _CanEagleAttack = true;
        
        animator.SetBool("EagleAttackDone1", true);
    }

    void EagleAttack2()
    {
        
        foreach (Enemy enemy in EnemyDectectorSphere.EnemiesDetectedSphere)
        {
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(30);
            enemy.GetComponent<Enemy>().Knockback(100);

        }
      
    }

    IEnumerator EagleAnime2()
    {
        _CanEagleAttack = false;
        animator.SetBool("EagleAttackDone2", false);
        yield return new WaitForSeconds(0.2f);
        _CanEagleAttack = true;
        
        animator.SetBool("EagleAttackDone2", true);
    }

    void EagleAttack3()
    {
       
        foreach (Enemy enemy in EnemyDectectorSphere.EnemiesDetectedSphere)
        {
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(60);
            enemy.GetComponent<Enemy>().Knockback(100);

        }
       
    }

    IEnumerator EagleAnime3()
    {
        _CanEagleAttack = false;
        animator.SetBool("EagleAttackDone3", false);
        yield return new WaitForSeconds(0.2f);
        _CanEagleAttack = true;
        
        animator.SetBool("EagleAttackDone3", true);
    }

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

    IEnumerator AnimNewEagleAttack()
    {
        _canNewEagleAttack = false;
        animator.SetBool("NewEagle", false);
        yield return new WaitForSeconds(2f);
        animator.SetBool("NewEagle", true);
        _canNewEagleAttack = true;

    }


    IEnumerator LynxAttack()
    {
        
     _canLynxAttack = false;
     
     rb.velocity = new Vector3(horizontalMovement, 0f, verticalMovement) * dashVelocity;

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
}

