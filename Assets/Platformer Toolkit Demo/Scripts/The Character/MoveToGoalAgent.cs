using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using GMTK.PlatformerToolkit;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{

    characterMovement _moveScript;
    characterJump _jumpScript;

    //GameOject _goalScript;
    GameObject _goal;

    public  Vector2 toGoal;
    double contadorvalue = 0f;

    void Awake()
    {
        _moveScript = GetComponent<characterMovement>();
        _jumpScript = GetComponent<characterJump>();
        //_goal = GetComponent<Goal>();

        toGoal =  Vector2.zero;
        
        //InstallPresetData();
    }
    /*
    private void Start()
    {
         _goal = GameObject.FindWithTag("Goal");
    }
    */
    public override void OnEpisodeBegin()
    {
        _moveScript.transform.position = new Vector2(-8f, -4f);
        //Debug.Log("ContadorNegativo" + contadorvalue);
    }

    public override void CollectObservations(VectorSensor sensor)
    {

        //toGoal = _goal.transform.position - _moveScript.transform.position; // not goal set rightnow...
        //sensor.AddObservation(toGoal.normalized);
        //sensor.AddObservation(_moveScript.velocity);

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // base.OnActionReceived(actions);

        int nr = actions.DiscreteActions[0]; // new rule
        int movement = actions.DiscreteActions[1]; // movement left or right
        int jump = actions.DiscreteActions[2]; // jump 


        if(actions.DiscreteActions[0] == 1)
        {
            //press button of new rule
            _jumpScript.canJumpAgain = true;
            //_jumpScript.jumping(true);
            //_jumpScript.pressing(true);
        }
        else
        {
            //none? or depress the button?
            _jumpScript.pressing(false);
        }

        if (actions.DiscreteActions[1] == 0) //left
        {
            _moveScript.directionX = -1;
            _moveScript.pressingKey = true;
            //_moveScript.maxSpeed = 0.5f;

        }
        else if(actions.DiscreteActions[1] == 1)//right
        {
            
            _moveScript.directionX = 1;
            _moveScript.pressingKey = true;

        }
        else // none
        {
            _moveScript.directionX = 0;
            _moveScript.pressingKey = false;
        }

        if(actions.DiscreteActions[2] == 1) // Jump
        {
            
            _jumpScript.jumping(true);
            _jumpScript.pressing(true);
        }
        else
        {
            _jumpScript.pressing(false);
        }


        //AddReward(-1f / MaxStep);

        contadorvalue = contadorvalue + (-2f / MaxStep);
        AddReward(-4f / MaxStep);
    }



   
    
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            discreteActions[1] = 0;
        }
        else if (Input.GetAxisRaw("Horizontal") > 0) 
        {
            discreteActions[1] = 1;
        }else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            discreteActions[1] = 2;
        }
        
        /*
        if(Input.GetKeyDown(KeyCode.Space))
        {
            discreteActions[2] = 1;
            Debug.Log("jump?");
        }else if (Input.GetKeyUp(KeyCode.Space))
        {
            discreteActions[2] = 0;
        }*/

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.TryGetComponent<Goal>(out Goal goal)) 
        {
            SetReward(+1f);
            EndEpisode();
        }
        */
        if (collision.CompareTag("Goal")) 
        {
            Debug.Log("goal reached.");
            SetReward(+5f);
            EndEpisode();
            //Debug.Log("collision with Goal");
        }

        if (collision.CompareTag("Wall")) 
        {
            SetReward(-1f);
            EndEpisode();
        }

        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("muerte");
            SetReward(-1f);
            EndEpisode();
        }
    }



}
