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
    private float _playerJumpForce = 5f;


    [Header("Raycast")]
    [SerializeField]
    private float _rayLength = 5f;
    [SerializeField]
    private GameObject _downObject;

    [Header("Bools")]
    [SerializeField]
    private bool _CanJump = true;


    [Header("PlayerInfo")]
    public Rigidbody rb;
    public GameObject render;

    [Header("Attack")]
    public Transform attackPoint;
    public Transform bearAttackPoint;
    [SerializeField]
    private float _attackRange = 5f;
    [SerializeField]
    private Vector3 _bearAttackRange;
    public LayerMask enemyLayer;


    // Raycast Variables
    Ray downRay;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        playerInstance = this;
    }

    // Update is called once per frame
    void Update()
    {


        // Reset jump with Raycast
        downRay = new Ray(_downObject.transform.position, Vector3.down * _rayLength);
        Debug.DrawRay(_downObject.transform.position, Vector3.down * _rayLength);

        if (Physics.Raycast(downRay, out RaycastHit groundInfo, _rayLength))
        {
            Debug.Log(groundInfo.collider.tag);
            if ( groundInfo.collider.tag == "Ground")
            {
                _CanJump = true;
            }
         
        }

        
        // Identification des mouvements
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        Debug.Log(horizontalMovement);
        Debug.Log(verticalMovement);

        // ce qui fait bouger le personnages
        rb.MovePosition(rb.position + new Vector3(horizontalMovement* _playerSpeed, 0f, verticalMovement* _playerSpeed) * Time.deltaTime);

        //direction du personnage : on fait rotate le render et non le player en tant que tel

        Vector3 playerDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (horizontalMovement <= -0.1)
        {
            render.transform.rotation = Quaternion.LookRotation(playerDirection);

        }
        if (horizontalMovement >= 0.1)
        {
            render.transform.rotation = Quaternion.LookRotation(playerDirection);
        }
        if (verticalMovement <= -0.1)
        {
            render.transform.rotation = Quaternion.LookRotation(playerDirection);

        }
        if (verticalMovement >= 0.1)
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

        // Jump

        if (Input.GetKeyDown(KeyCode.JoystickButton0) && _CanJump == true)
        {
            Debug.Log("jump");
            rb.velocity = new Vector3 (0f, _playerJumpForce, 0f);
            _CanJump = false;

        }


        // Simple Attack
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Attack();
        }

        //Bear Attack
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            BearAttack();
        }

        //TigerAttack
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            StartCoroutine(TigerAttack());
        }
    }

void Attack ()
    {
       Collider[] hitEnemies =  Physics.OverlapSphere(attackPoint.position, _attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("You hit Enemies");
            enemy.GetComponent<Enemy>().TakeDamamge(20);
            enemy.GetComponent<Enemy>().Knockback(50);

        }
    }


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
        foreach (Enemy enemy in EnemyDetector.EnemiesDetected)
        {
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(40);
            enemy.GetComponent<Enemy>().Knockback(200);

        }
    }


    IEnumerator TigerAttack()
    {
        int l = EnemyDetector.EnemiesDetected.Count;
       /* foreach (Enemy enemy in Detector.EnemiesDetected)
        {
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(20);
            enemy.Knockback(50);
            
        }*/

        for (int i = l - 1; i >= 0; i--)
        {
            Enemy enemy = EnemyDetector.EnemiesDetected[i];
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
        l = EnemyDetector.EnemiesDetected.Count;
        for (int i= l-1; i >=0 ; i--)
        {
            Enemy enemy = EnemyDetector.EnemiesDetected[i];
            Debug.Log("You hit Enemies");
            enemy.TakeDamamge(20);
            enemy.Knockback(50);
        }
    } 
   /* private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(Vector3.zero,render.transform.rotation, Vector3.one);
        Gizmos.DrawWireSphere(attackPoint.position, _attackRange);
        Gizmos.DrawWireCube(bearAttackPoint.position, _bearAttackRange);
        //Gizmos.DrawSphere(transform.position, 10);
        //Handles.DrawCube(0, bearAttackPoint.position, Quaternion.Euler(0, 45, 0), _bearAttackRange);
        
    }*/

}

