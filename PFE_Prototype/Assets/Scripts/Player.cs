using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Mouvements")]
    [SerializeField]
    private float _playerSpeed = 5f;
    [SerializeField]
    private float _playerMaxSpeed = 20f;
    [SerializeField]
    private float _playerSpeedIncr = 0.90f;
    [SerializeField]
    private float _playerSpeedDecr = 0.90f;
    [SerializeField]
    private float _stopLimite = 5f;



    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");


        if (horizontalMovement > 0 || verticalMovement >0)
        {
            if (_playerSpeed < 0) _playerSpeed = 0;

            if (Mathf.Abs(_playerSpeed) < _playerMaxSpeed)
            {
                _playerSpeed += (Time.deltaTime * _playerSpeedIncr);
                if (Mathf.Abs(_playerSpeed) > _playerMaxSpeed)
                {
                    _playerSpeed = _playerMaxSpeed;
                }
            }

        }
        else if (horizontalMovement < 0 || verticalMovement < 0)
        {
            if (_playerSpeed > 0) _playerSpeed = 0;


            if (Mathf.Abs(_playerSpeed) < _playerMaxSpeed)
            {
                _playerSpeed -= (Time.deltaTime * _playerSpeedIncr);
                if (Mathf.Abs(_playerSpeed) > _playerMaxSpeed)
                {
                    _playerSpeed = -_playerMaxSpeed;
                }
            }

        }
        // speed decrease
        else
        {
            _playerSpeed *= _playerSpeedDecr;   // speedDecr entre 0 et 1
            if (Mathf.Abs(_playerSpeed) < _stopLimite)
            {
                _playerSpeed = 0;
            }
        }
    }


    private void FixedUpdate()
    {
        PlayerMove();
    }


    private void PlayerMove() 
    {
        rb.MovePosition(transform.position + new Vector3(_playerSpeed, 0, 0) * Time.deltaTime);
    }
}
