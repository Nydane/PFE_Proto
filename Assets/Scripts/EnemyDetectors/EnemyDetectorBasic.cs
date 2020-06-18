using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetectorBasic : MonoBehaviour
{
    public static List<Enemy> EnemiesDetectedBasic = new List<Enemy>();


    public void OnTriggerEnter(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            EnemiesDetectedBasic.Add(e);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            EnemiesDetectedBasic.Remove(e);
        }
    }
}

