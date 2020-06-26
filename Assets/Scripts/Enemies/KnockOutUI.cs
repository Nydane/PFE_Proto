using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnockOutUI : MonoBehaviour
{
    // Start is called before the first frame update

    public Image knock1;
    public Image knock2;
    public Image knock3;

    public Enemy enemy;
    
    void Start()

    {
        enemy = GetComponentInParent<Enemy>();
        knock1.enabled = false;
        knock2.enabled = false;
        knock3.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {

        if (Player.playerInstance._isCarrying == false)
        {
            if (enemy.knockOutCount == 1)
            {
                knock1.enabled = true;

            }
            else if (enemy.knockOutCount == 2)
            {
                knock1.enabled = true;
                knock2.enabled = true;

            }
            else if (enemy.knockOutCount == 3)
            {
                knock1.enabled = true;
                knock2.enabled = true;
                knock3.enabled = true;

            }
            else if (enemy.knockOutCount == 0)
            {
                knock1.enabled = false;
                knock2.enabled = false;
                knock3.enabled = false;
            }
        }

        if (Player.playerInstance._isCarrying == true)
        {
            knock1.enabled = false;
            knock2.enabled = false;
            knock3.enabled = false;
        }


    }
}
