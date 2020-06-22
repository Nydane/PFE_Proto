using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    private Rigidbody _rb;
    public Transform a, b;
    [Range(0,1)]
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, 1);
        transform.position = Vector3.Lerp(a.position, b.position, pingPong);

    }
}
