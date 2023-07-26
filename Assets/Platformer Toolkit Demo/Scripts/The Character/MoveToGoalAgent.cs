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

    public GameObject _goalScript;


    private Vector2 toGoal;

    void Awake()
    {
        _moveScript = GetComponent<characterMovement>();
        _jumpScript = GetComponent<characterJump>();
        //_goalScript = GetComponent<Goal>();

        toGoal =  Vector2.zero;

        //InstallPresetData();
    }

    public override void OnEpisodeBegin()
    {
        _moveScript.transform.position = new Vector2(-3f, 0f);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        
        toGoal = _goalScript.transform.position - _moveScript.transform.position;
        sensor.AddObservation(toGoal.normalized);

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
        }else
        { 
            //none? or depress the button?
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


        AddReward(-1f / MaxStep);
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
            SetReward(+1f);
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
            SetReward(-1f);
            EndEpisode();
        }
    }



}
