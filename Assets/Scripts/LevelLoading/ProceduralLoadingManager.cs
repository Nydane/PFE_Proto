using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ProceduralLoadingManager : MonoBehaviour
{

   


    private static ProceduralLoadingManager _instance;
    public static ProceduralLoadingManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("No instance Found");

            }

            return _instance;
        }
    }

    public List<int> levelPoolUniqueNumbers = new List<int>();
    public List<int> levelPoolList = new List<int>();
    public int numberOfLevelInThePool = 4;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(gameObject);
       

    }

    public void Start()
    {


        PoolOfLevel();
        

    }
    

    public void PoolOfLevel()
    {
        for (int i = 0; i < numberOfLevelInThePool; i++)
        {
            levelPoolUniqueNumbers.Add(i);
        }
        for (int i = 0; i < numberOfLevelInThePool; i++)
        {
            int ranNum = levelPoolUniqueNumbers[Random.Range(0, levelPoolUniqueNumbers.Count)];
            levelPoolList.Add(ranNum);
            levelPoolUniqueNumbers.Remove(ranNum);
        }
    }
}
