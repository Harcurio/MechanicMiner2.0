using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 2.75f;    

    private bool goRight;

    private float timeCounter;

    private float timeToChange = 4f;// 4 default


    bool change = false;

    void Start()
    {
        timeCounter = timeToChange; 
    }

    void differentTime() 
    { 
        timeToChange = Random.Range(1f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        if(goRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        timeCounter -= Time.deltaTime;

        if(timeCounter <= 0)
        {
            
            timeCounter = timeToChange;
            goRight = !goRight;
            change = !change;
            /*
            if (!change)
            {
                differentTime();
            }*/
        }
    }
}
