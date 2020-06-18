using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectorEagle : MonoBehaviour
{
    public static List<Enemy> EnemiesDetectedEagle = new List<Enemy>();


    public void OnTriggerEnter(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            EnemiesDetectedEagle.Add(e);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            EnemiesDetectedEagle.Remove(e);
        }
    }
}
