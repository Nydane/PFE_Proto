using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockOutBox : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody enemyRB;
    public GameObject enemy;
    public GameObject carryDestination;
    public KnockOutUI uiScript;
    public Enemy enemyScript;
    public Collider enemyCollider;

    void Start()
    {
        //enemyRB = GetComponentInParent<Rigidbody>();
        //enemy = GetComponentInParent<GameObject>();
        //enemyScript = GetComponentInParent<Enemy>();
        //enemyCollider = GetComponentInParent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && enemyScript.isKnockOut == true)
        {
            Player.playerInstance._isCarrying = true;

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (enemyScript.isKnockOut == true)
        {
            if (other.tag == "Player" && Input.GetKey(KeyCode.Joystick1Button2))
            {
                enemyRB.useGravity = false;
                enemy.transform.position = carryDestination.transform.position;
                enemy.transform.rotation = Quaternion.Euler(0, 0, 90);
                enemy.transform.parent = GameObject.Find("CarryPoint").transform;
                Player.playerInstance._isCarrying = true;
                enemyCollider.enabled = false;

            }


            if (other.tag == "Player" && Input.GetKeyUp(KeyCode.Joystick1Button2))
            {
                enemyRB.useGravity = true;
                enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
                enemy.transform.parent = GameObject.Find("BadBoBFollow").transform;
                enemy.transform.localScale = new Vector3(1, 1.8f, 1);
                Player.playerInstance._isCarrying = false;
                var direction = (Player.playerInstance.venomArrowPos.position - Player.playerInstance.transform.position); // utilisation du point de lancement des flèches pour donner une direction au lancer
                enemyRB.AddForce(direction.normalized * Player.playerInstance.throwPower, ForceMode.Impulse);
                enemyCollider.enabled = true;

            }
        }
        else if (enemyScript.isKnockOut == false)
        {
            enemyRB.useGravity = true;
            enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
            enemy.transform.parent = GameObject.Find("BadBoBFollow").transform;
            enemy.transform.localScale = new Vector3(1, 1.8f, 1);
            Player.playerInstance._isCarrying = false;
            enemyCollider.enabled = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enemyRB.useGravity = true;
            enemy.transform.rotation = Quaternion.Euler(0, 0, 0);
            enemy.transform.parent = GameObject.Find("BadBoBFollow").transform;
            enemy.transform.localScale = new Vector3(1, 1.8f, 1);
            Player.playerInstance._isCarrying = false;
            enemyCollider.enabled = true;

        }


    }

}
