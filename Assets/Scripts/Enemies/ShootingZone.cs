using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingZone : MonoBehaviour
{
    public EnemyBehavior enemyBehaviorScript;
    

    // Start is called before the first frame update
    void Start()
    {
        enemyBehaviorScript = transform.GetComponentInParent<EnemyBehavior>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyBehaviorScript.isRunning)
        {
            enemyBehaviorScript.UpdateEnemyState(ENEMY_STATE.RUNNING);
            enemyBehaviorScript.enemyState = ENEMY_STATE.RUNNING;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyBehaviorScript.isRunning = true;
            enemyBehaviorScript.isShooting = false;
            enemyBehaviorScript.isPatrolling = false;
        }
    }
}
