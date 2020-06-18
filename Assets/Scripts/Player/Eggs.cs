using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eggs : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    public SphereCollider col;

    public float timeAliveMax = 5f;
    public float timeAlive;


    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
        rb.velocity = transform.up * -speed;
        

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
            col.radius = 2f;
            other.GetComponent<Enemy>().TakeDamamge(20);

            //Destroy(gameObject);
        }

        if (other.tag == "Ground")
        {
            col.radius = 2f;
            //Destroy(gameObject);

        }
    }
}
