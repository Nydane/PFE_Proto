using System.Collections;
using System.Collections.Generic;
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


    public void Knockback (float KnockPower)
    {
        var knockDirection = transform.position - Player.playerInstance.transform.position;
        
        rbEnemy.AddForce(knockDirection.normalized * KnockPower, ForceMode.Impulse);
        //rbEnemy.AddForce((rbEnemy.velocity * KnockPower)*-1 , ForceMode.Impulse);

    }
}
