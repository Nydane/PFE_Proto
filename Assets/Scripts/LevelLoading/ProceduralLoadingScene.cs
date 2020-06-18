using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProceduralLoadingScene : MonoBehaviour
{
   // public ProceduralLoadingManager procedualLoadingManager;

    public int loadLevel;
    public static int nextLevel=0;
    
    private void Start()
    {
        
        //procedualLoadingManager = GetComponent<ProceduralLoadingManager>();
        
    }
    private void Update()
    {
       //loadLevel = ProceduralLoadingManager.levelPoolList[nextLevel];
       loadLevel = ProceduralLoadingManager.Instance.levelPoolList[nextLevel];

    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Player")
        {

            //SceneManager.LoadScene(ProceduralLoadingManager.levelPoolList[nextLevel]);
            SceneManager.LoadScene(ProceduralLoadingManager.Instance.levelPoolList[nextLevel]);
            nextLevel++;
            /*if (nextLevel >= ProceduralLoadingManager.levelPoolList.Count)
            {
                nextLevel = 0;
            }*/
            if (nextLevel >= ProceduralLoadingManager.Instance.levelPoolList.Count)
            {
                nextLevel = 0;
            }


        }
        /*for (int i = 0; i < ProceduralLoadingManager.levelPoolList.Count; i++)
        {
            SceneManager.LoadScene(ProceduralLoadingManager.levelPoolList[i]);
            i++;
        }*/
    }


   
}
