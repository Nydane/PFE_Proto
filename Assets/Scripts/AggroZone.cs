using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroZone : MonoBehaviour
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
        if (enemyBehaviorScript.isShooting)
        {
            enemyBehaviorScript.UpdateEnemyState(ENEMY_STATE.SHOOTING);
            enemyBehaviorScript.enemyState = ENEMY_STATE.SHOOTING;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyBehaviorScript.isShooting = true;
            enemyBehaviorScript.isRunning = false;
            enemyBehaviorScript.isPatrolling = false;

        }
    }
}
