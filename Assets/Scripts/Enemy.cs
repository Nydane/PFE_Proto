using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int currentEnemyHealth;
    public int maxenemyHealth = 100;
    private Player _player;
    private Rigidbody rbEnemy;
    public HealthBar healthBar;
    public bool hasBehavior;
    public EnemyBehavior enemyBehaviorScript;



    // Start is called before the first frame update
    void Start()
    {
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
        gameObject.SetActive(false);
    }

    public void Knockback (int KnockPower)
    {
        var knockDirection = transform.position - Player.playerInstance.transform.position;
        rbEnemy.AddForce(knockDirection * KnockPower);
    }
}
