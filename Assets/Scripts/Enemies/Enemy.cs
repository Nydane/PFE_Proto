using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    public int currentEnemyHealth;
    public int maxenemyHealth = 100;
    public HealthBar healthBar;

    [Header("Pomme")]
    private Player _player;
    private Rigidbody rbEnemy;
    private Renderer _renderer;
    public Material enemyMaterial;

    [Header("Bool")]
    public bool hasBehavior;

    [Header("Script")]

    public EnemyBehavior enemyBehaviorScript;


    [Header("Paralyzed")]
    public bool isParalyzed = false;
    public float ParalyzedTimer = 3f;
    public Material ParalyzedMaterial;


    [Header("KnockOut")]
    public int numberToGetKnockOut = 3;
    public int knockOutCount;
    public float timeKO = 5f;
    public float knockOutTimer;
    public bool isKnockOut = false;
    public Renderer boxKnockOut;
    public Material knockOutMaterial;

    [Header("Getting Attacked")]
    public bool isGettingAttacked = false;
    public float isGettingAttackedTimer;

    [Header("Getting Attacked")]
    public int doingDamage = 10;



    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();

        if (hasBehavior)
        {
            enemyBehaviorScript = GetComponent<EnemyBehavior>();

        }
        currentEnemyHealth = maxenemyHealth;
        healthBar.SetMaxHealth(currentEnemyHealth);
        rbEnemy = transform.GetComponent<Rigidbody>();

    }

    // Update is called once per frame



    void Update()
    {
        // Logique pour le knockOut
         if (knockOutCount > 0)
         {
            
            knockOutTimer -= Time.deltaTime;
            
            if (knockOutTimer <= 0)
            {
                SetKOState(false);
            }

         }

    }

    public IEnumerator IsGettingAttackedForAurelien()
    {
        isGettingAttacked = true;
        yield return new WaitForSeconds(isGettingAttackedTimer);
        isGettingAttacked = false;

    }

    public void TakeDamamge (int Damage)

    {
       currentEnemyHealth -= Damage;
       healthBar.SetHealth(currentEnemyHealth);
      
        
        if (currentEnemyHealth <= 0)
        {
            if (hasBehavior)
            {
                enemyBehaviorScript.UpdateEnemyState(ENEMY_STATE.DYING);
                enemyBehaviorScript.enemyState = ENEMY_STATE.DYING;
            }         
            else Die();
        }
    }

    /*public IEnumerator DoDamage()
    {
        yield return new WaitForSeconds(1);
        Player.playerInstance.DamageTaken(doingDamage);
    }*/

    public void Die()
    {
        Debug.Log("You killed an enemy");
        EnemyDetectorRectangle.EnemiesDetectedRectangle.Remove(this);
        EnemyDectectorSphere.EnemiesDetectedSphere.Remove(this);
        EnemyDetectorBear.EnemiesDetectedBear.Remove(this);
        EnemyDetectorLynx.EnemiesDetectedLynx.Remove(this);
        EnemyDetectorEagle.EnemiesDetectedEagle.Remove(this);
        EnemyDetectorBasic.EnemiesDetectedBasic.Remove(this);
        gameObject.SetActive(false);
    }

    IEnumerator Paralyzed()
    {
        isParalyzed = true;
        _renderer.material = ParalyzedMaterial;
        yield return new WaitForSeconds(ParalyzedTimer);
        isParalyzed = false;
        _renderer.material = enemyMaterial;

    }

    public void KnockOut(int NumberOfMarkerAdded)
    {
        knockOutTimer = timeKO;
        knockOutCount += NumberOfMarkerAdded;

        if (knockOutCount >= numberToGetKnockOut)
        {
            SetKOState(true);
        }

    }

   public void SetKOState(bool isKO)
    {
        if (isKO == false)
        {
            knockOutCount = 0;
            knockOutTimer = 0;
            isKnockOut = false;
            _renderer.material = enemyMaterial;
        }
        else
        {
            isKnockOut = true;
            knockOutTimer = timeKO;
            knockOutCount = numberToGetKnockOut;
            _renderer.material = knockOutMaterial;

        }
    }

    public void Knockback (float KnockPower)
    {
        var knockDirection = transform.position - Player.playerInstance.transform.position;
        
        rbEnemy.AddForce(knockDirection.normalized * KnockPower, ForceMode.Impulse);

    }
}
