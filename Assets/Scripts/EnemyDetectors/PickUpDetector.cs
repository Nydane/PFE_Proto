using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDetector : MonoBehaviour
{
    public static List<Enemy> pickUpDetectorList = new List<Enemy>();
    public List<Enemy> debugList = new List<Enemy>();


    private void Update()
    {
        debugList = pickUpDetectorList;
    }
    public void OnTriggerEnter(Collider other)
    {

        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            pickUpDetectorList.Add(e);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Enemy e = other.GetComponent<Enemy>();
        if (e != null)
        {
            
            
             pickUpDetectorList.Remove(e);

            
        }
    }
}
