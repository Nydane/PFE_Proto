using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectorBear : MonoBehaviour
{
    public static List<Enemy> EnemiesDetectedBear = new List<Enemy>();


    public void OnTriggerEnter(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            EnemiesDetectedBear.Add(e);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            EnemiesDetectedBear.Remove(e);
        }
    }
}
