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



    // Start is called before the first frame update
    void Start()
    {
        currentEnemyHealth = maxenemyHealth;
        healthBar.SetMaxHealth(currentEnemyHealth);
        rbEnemy = transform.GetComponent<Rigidbody>();

    }

    // Update is called once per frame



    void Update()
    {
        Debug.Log(currentEnemyHealth);
    }

    public void TakeDamamge (int Damage)

    {
       currentEnemyHealth -= Damage;
       healthBar.SetHealth(currentEnemyHealth);
      
        
        if (currentEnemyHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("You killed an enemy");
        EnemyDetectorRectangle.EnemiesDetectedRectangle.Remove(this);
        gameObject.SetActive(false);
    }

    public void Knockback (int KnockPower)
    {
        var knockDirection = transform.position - Player.playerInstance.transform.position;
        rbEnemy.AddForce(knockDirection * KnockPower);
    }
}
