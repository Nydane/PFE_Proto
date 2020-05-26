using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{

    [SerializeField]
    private bool _CanJump = true;
    [SerializeField]
    private float _playerJumpForce = 5f;
    [SerializeField]
    private float _jumpFall = 10f;
    public float fallMultiplier= 2.5f;
    public float lowJumpMultiplier = 2f;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.JoystickButton0) && _CanJump == true)
        {


            Debug.Log("jump");
            rb.velocity = new Vector3(0f, _playerJumpForce, 0f);
            _CanJump = false;
        }




        if (rb.velocity.y <0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y* (fallMultiplier - 1) *Time.deltaTime;
            
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Joystick1Button0))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * (Time.deltaTime * _jumpFall);
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _CanJump = true;

        }
    }
}
