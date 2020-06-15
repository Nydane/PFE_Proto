using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public GameObject target;
    private Rigidbody rb;
    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        moveDir = (target.transform.position - transform.position).normalized * speed;
        rb.velocity = new Vector3(moveDir.x, moveDir.y, moveDir.z);
        Destroy(gameObject, 10f);
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            Debug.Log("Player is hit");
        }
        else if (other.tag == "Gound")
        {
            Destroy(gameObject);
            Debug.Log("Arrow vs Wall : Wall Wins");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
