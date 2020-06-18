using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectorLynx : MonoBehaviour
{
    public static List<Enemy> EnemiesDetectedLynx = new List<Enemy>();


    public void OnTriggerEnter(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            EnemiesDetectedLynx.Add(e);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            EnemiesDetectedLynx.Remove(e);
        }
    }
}
