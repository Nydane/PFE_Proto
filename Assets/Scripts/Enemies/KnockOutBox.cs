using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockOutBox : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject enemy;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.Joystick1Button2))
        {
            //enemy.transform.position
        }
    }
}
