using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDectectorSphere : MonoBehaviour
{
    public static List<Enemy> EnemiesDetectedSphere = new List<Enemy>();


    public void OnTriggerEnter(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            EnemiesDetectedSphere.Add(e);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            EnemiesDetectedSphere.Remove(e);
        }
    }
}
