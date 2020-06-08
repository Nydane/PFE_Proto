using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eggs : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
        rb.velocity = transform.up * -speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {

            other.GetComponent<Enemy>().TakeDamamge(20);
            Destroy(gameObject);
        }

        if (other.tag == "Ground")
        {
            Destroy(gameObject);

        }
    }
}
