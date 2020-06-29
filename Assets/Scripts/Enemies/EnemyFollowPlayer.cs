using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{

    public Enemy enemy;
    public float speed;
    private Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.isParalyzed == false && enemy.isKnockOut == false && enemy.isGettingAttacked ==false)
 
        {
            Vector3 moveDirection = Player.playerInstance.transform.position - transform.position;
            _rb.MovePosition(transform.position + moveDirection.normalized * speed * Time.deltaTime);
        }
        
    }
}
