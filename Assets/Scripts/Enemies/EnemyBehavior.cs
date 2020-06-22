using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [Header("Pomme")]
    private Rigidbody _rb;
    public float speed;
    public Enemy enemyScript;
    public Animator animator;
    public AggroZone aggroZone;
    public ShootingZone shootingZone;
    public EndAggroZone endAggroZone;
    private GameObject target;
    public NavMeshAgent agent;

    [Header("StateBooleans")]
    public bool isShooting = false;
    public bool isPatrolling = true;
    public bool isDying = false;
    public bool isRunning= false;
    public ENEMY_STATE enemyState;

    [Header("Arrow")]
    public GameObject arrow;
    public Transform arrowPos;
    public float fireRate;
    private float time;

    [Header("Patrolling")]
    public Transform[] moveSpots;
    private int randomSpot;
    public float waitTime;
    public float startWaitTime;
    public float patrollingSpeed = 5f;

    [Header("Running")]
    public float runningSpeed;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyScript = transform.GetComponent<Enemy>();
        _rb = GetComponent<Rigidbody>();
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);

        target = GameObject.FindGameObjectWithTag("Player");
        aggroZone = gameObject.GetComponentInChildren<AggroZone>();
        shootingZone = gameObject.GetComponentInChildren<ShootingZone>();
        endAggroZone = gameObject.GetComponentInChildren<EndAggroZone>();
    }


    // Update is called once per frame
    void Update()
    {
        


    }
    
    
    void Patrolling()
    {
        //speed = patrollingSpeed;
        Debug.Log("Patrolling");
        //transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        //Quaternion enemyRota = Quaternion.LookRotation(moveSpots[randomSpot].position - transform.position);
        //transform.rotation = enemyRota;
        agent.SetDestination(moveSpots[randomSpot].position);
        agent.speed = patrollingSpeed;
        //animator.Play("Walking");
        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            
            
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
                


            }
        }
    }


    
    public void BehaviorDie()
    {
   
            Debug.Log("You killed an enemy");
            EnemyDetectorRectangle.EnemiesDetectedRectangle.Remove(enemyScript);
            EnemyDectectorSphere.EnemiesDetectedSphere.Remove(enemyScript);
            EnemyDetectorBear.EnemiesDetectedBear.Remove(enemyScript);
            EnemyDetectorLynx.EnemiesDetectedLynx.Remove(enemyScript);
            EnemyDetectorEagle.EnemiesDetectedEagle.Remove(enemyScript);
            Destroy(gameObject);
  
    }

    void Shooting()
    {
       
        NavMeshHit hit;
        if (agent.Raycast(target.transform.position, out hit))
        {
            agent.SetDestination(target.transform.position);
            agent.speed = runningSpeed;

        }
        else if (!agent.Raycast(target.transform.position, out hit))
        {
            time += Time.deltaTime;
            Quaternion arrowRota = Quaternion.LookRotation(target.transform.position - arrowPos.transform.position);
            Quaternion enemyRota = Quaternion.LookRotation(target.transform.position - transform.position);
            
            
            agent.speed = 0f;
            agent.transform.rotation = enemyRota;
            
            if (time > fireRate)
            {
                Instantiate(arrow, arrowPos.position, arrowRota);
                time = 0f;
            }
        }
       
    }


    void Running()
    {
        //speed = runningSpeed;
        Debug.Log("Running");
        //Vector3 moveDirection = Player.playerInstance.transform.position - transform.position;
        //_rb.MovePosition(transform.position + moveDirection.normalized * speed * Time.deltaTime);
       // Quaternion enemyRota = Quaternion.LookRotation(target.transform.position - transform.position);
       // transform.rotation = enemyRota;

        agent.SetDestination(target.transform.position);
        agent.speed = runningSpeed;
    }

   public void UpdateEnemyState (ENEMY_STATE enemyState)
    {
        // State Machine
        switch (enemyState)
        {
            case ENEMY_STATE.PATROLLING:
                Patrolling();
                break;
            case ENEMY_STATE.RUNNING:
                Running();
                break;
            case ENEMY_STATE.DYING:
                //Die(2f);
                Debug.Log("Death");
                aggroZone.gameObject.SetActive(false);
                endAggroZone.gameObject.SetActive(false);
                shootingZone.gameObject.SetActive(false);
                isShooting = false;
                isRunning = false;
                isPatrolling = false;
                isDying = true;
                //animator.Play("Death");
                BehaviorDie();
                break;
            case ENEMY_STATE.SHOOTING:
                Debug.Log("Shooting");
                Shooting();
                break;
            default:
                break;

        }
    }


    
}

public enum ENEMY_STATE
{
    PATROLLING,
    RUNNING,
    SHOOTING,
    DYING
    
}
