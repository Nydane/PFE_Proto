using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonArrow : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;

    public float timeAliveMax = 5f;
    public float timeAlive;
    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

    }
    private void Update()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive > timeAliveMax)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().StartCoroutine("Paralyzed");

            Destroy(gameObject);
        }

        
    }
}
