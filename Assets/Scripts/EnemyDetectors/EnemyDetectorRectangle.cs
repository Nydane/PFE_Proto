using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyDetectorRectangle : MonoBehaviour
{
    public static List<Enemy> EnemiesDetectedRectangle = new List<Enemy>();
    

    public void OnTriggerEnter(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            EnemiesDetectedRectangle.Add(e);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            EnemiesDetectedRectangle.Remove(e);
        }
    }
}
