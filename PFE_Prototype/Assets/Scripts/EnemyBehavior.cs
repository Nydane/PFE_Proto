using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    private Rigidbody _rb;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
       
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 moveDirection = Player.playerInstance.transform.position - transform.position;
        
        _rb.MovePosition(transform.position + moveDirection.normalized * speed*Time.deltaTime);
    }
}
