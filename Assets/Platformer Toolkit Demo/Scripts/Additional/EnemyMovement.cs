using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float speed = 2.75f;    

    public bool goRight;

    private float timeCounter;

    private float timeToChange = 2f;// 4 default

    public Vector2 previousPosition;
    public Vector2 localVelocity;

    bool change = false;

    void Start()
    {
        timeCounter = timeToChange;
        previousPosition = transform.position;
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

        // Calculate the change in position
        Vector2 positionChange = (Vector2)transform.position - previousPosition;

        // Calculate the local velocity (change in position / change in time)
        localVelocity = positionChange / Time.deltaTime;

        // Store the current position for the next frame
        previousPosition = (Vector2)transform.position;

        // Use localVelocity as needed (e.g., print it to the console)
        //Debug.Log("Local Velocity: " + localVelocity);
    }
}
