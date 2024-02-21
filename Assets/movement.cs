using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTK.PlatformerToolkit;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 true_location;
    public Vector2 old_location;


    characterMovement _moveScript;

    void Start()
    {
        true_location = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 objectPosition2D = new Vector2(transform.position.x, transform.position.y);


        if (objectPosition2D != true_location) 
        {
            old_location = true_location;
            true_location = transform.position;
        }
        
        
        
    }
}
