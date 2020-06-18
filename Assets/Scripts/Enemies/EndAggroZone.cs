using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAggroZone : MonoBehaviour
{
    public EnemyBehavior enemyBehaviorScript;
    

    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviorScript = transform.GetComponentInParent<EnemyBehavior>();
    }

    private void Update()
    {
        if (enemyBehaviorScript.isPatrolling)
        {
            enemyBehaviorScript.UpdateEnemyState(ENEMY_STATE.PATROLLING);
            enemyBehaviorScript.enemyState = ENEMY_STATE.PATROLLING;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            enemyBehaviorScript.isPatrolling = true;
            enemyBehaviorScript.isShooting = false;
            enemyBehaviorScript.isRunning = false;
            
        }
    }
}
