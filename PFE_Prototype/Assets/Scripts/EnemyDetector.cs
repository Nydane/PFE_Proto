using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDetector : MonoBehaviour
{
    public static List<Enemy> EnemiesDetected = new List<Enemy>();
    

    public void OnTriggerEnter(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            EnemiesDetected.Add(e);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            EnemiesDetected.Remove(e);
        }
    }
}
