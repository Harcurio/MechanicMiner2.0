using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using GMTK.PlatformerToolkit;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{

    characterMovement _moveScript;
    characterJump _jumpScript;
    movement _newmov; //correction of the movement ? by optaining the movement from other place...
    characterJuice _juice_script;


    public Rigidbody2D playerrb;
    //public Rigidbody2D enemyrb; 
    
    public EnemyGrammar _enemyStript; //script!! 

    //GameOject _goalScript;
    GameObject _goal;

    //counter to change the enemy
    int countEpisodes = 0;

    //public  Vector2 toGoal;
    //double contadorvalue = 0f;

    //count for goal, enemy...
    // JSON file
    string name_file = "logs_player.json";
    //string filePath = Application.persistentDataPath + ;
    playerData logsPlayer;
    int goal_reached = 0;
    int killed = 0;

    void Awake()
    {
        _moveScript = GetComponent<characterMovement>();
        _jumpScript = GetComponent<characterJump>();
        _juice_script = GetComponent<characterJuice>();
        _newmov = GetComponent<movement>();
        
        //playerrb = GetComponent<>();
        //_enemyStript = GetComponent<EnemyGrammar>();
        //_goal = GetComponent<Goal>();

        //toGoal = Vector2.zero;

        //InstallPresetData();

        //Json initialization saved info
        logsPlayer = new playerData();

        generateJSON();

    }
    
    public override void OnEpisodeBegin()
    {

        float ranX = Random.Range(-15f,27f);
        _moveScript.transform.position = new Vector2(ranX, -2f); //-2 to train both models
        //_enemyStript.movePosition();
        //_enemyStript.GenerateEnemy();   //  generate random enemies....
        _enemyStript.GenerateEnemyFromFile(); //load enemy from file

        
        countEpisodes++;
    }

    
    public void generateJSON()
    {

        //Debug.Log("GENERANDO JSON....?");
        //logsPlayer.times_killed = killed;
        //logsPlayer.times_won = goal_reached;


        string json = JsonUtility.ToJson(logsPlayer);

        string savePath = Path.Combine(Application.dataPath, name_file);
       // Debug.Log(Application.dataPath);
       // Debug.Log(savePath);
        File.WriteAllText(savePath, json);
        Debug.Log("logs saved at " + savePath);
    }
    
    public void UpdateJSON(bool won)
    {
        string savePath = Path.Combine(Application.dataPath, name_file);

        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);

            logsPlayer = JsonUtility.FromJson<playerData>(json);

            if (won)
            {
                logsPlayer.times_won.Add(1);
                logsPlayer.total_times_won = goal_reached;
            }
            else 
            {
                logsPlayer.times_won.Add(0);
            }

            Vector2 _tem = new Vector2(_enemyStript.test.transform.position.x, _enemyStript.test.transform.position.y);
            Vector2 _tem1 = new Vector2(transform.position.x, transform.position.y);
            Vector2 _tem2 = new Vector2(_newmov.true_location.x, _newmov.true_location.y);
            //Vector2 _tem3 = new Vector2(_newmov.old_location.x, _newmov.old_location.y);
            Vector2 tran_tem = _enemyStript.positionEnemy.position - transform.position;


            Vector2 velocityplayer = playerrb.velocity;
            Vector2 velocityrelativeplayer = velocityplayer - _enemyStript.test.localVelocity;
            //Debug.Log("Local Velocity: " + _enemyStript.test.localVelocity);
            //Debug.Log("local velocity player"+ velocityrelativeplayer);



            Vector2 posicionEnemigo = EnemyLocation.Instancia.ObtenerPosicionEnemigo();


            //Vector2 relative_velocity = 
            Vector2 localenemy = _tem - _tem2;
            Vector2 localplayer = _tem2 - posicionEnemigo;


            
            
            Debug.Log("enemy" + _tem);
            Debug.Log("player" + _tem2.ToString());
            Debug.Log("enemynew" + posicionEnemigo.ToString());
            Debug.Log("local pos" + localplayer);

            logsPlayer.local_position_enemy.Add(localenemy);
            logsPlayer.local_position_player.Add(localplayer);
            logsPlayer.direction_player.Add(tran_tem);
            logsPlayer.local_player_velocity.Add(velocityrelativeplayer);

            string updatedJson = JsonUtility.ToJson(logsPlayer);
            File.WriteAllText(savePath, updatedJson);
            Debug.Log("JSON data updated and saved.");
        }
    }
    

    public override void CollectObservations(VectorSensor sensor)
    {

        //toGoal = _goal.transform.position - _moveScript.transform.position; // not goal set rightnow...
        //sensor.AddObservation(toGoal.normalized);
        sensor.AddObservation(_moveScript.velocity);

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // base.OnActionReceived(actions);

        int nr = actions.DiscreteActions[0]; // new rule
        int movement = actions.DiscreteActions[1]; // movement left or right
        int jump = actions.DiscreteActions[2]; // jump 


        if(actions.DiscreteActions[0] == 1) // new rule
        {
            //press button of new rule
            //Debug.Log("double jump");
            //_jumpScript.doubleJump();//canJumpAgain = true;
            _jumpScript.jumping(true);
            _jumpScript.pressing(true);
        }

        if (actions.DiscreteActions[2] == 1) // Jump
        {

            _jumpScript.jumping(true);
            _jumpScript.pressing(true);
        }


        //else
        //{
        //none? or depress the button?
        //  _jumpScript.pressing(false);
        //}

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

        
        /*else
        {
            //_jumpScript.pressing(false);
            Debug.Log("se esta precionando esto");
        }*/


        //AddReward(-1f / MaxStep);

        //contadorvalue = contadorvalue + (-2f / MaxStep);
        AddReward(-3.5f / MaxStep);
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
        
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
           // discreteActions[2] = 1;
            //Debug.Log("jump?");
        }else if (Input.GetKeyUp(KeyCode.Space))
        {
            //discreteActions[2] = 0;
        }

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal")) 
        {
            goal_reached++;
            UpdateJSON(true);
            SetReward(+5f);
            Debug.Log("goal reached.");
            EndEpisode();
        }

        if (collision.CompareTag("Wall")) 
        {
            SetReward(-1f);
            EndEpisode();
        }

        if (collision.CompareTag("Enemy"))
        {
            UpdateJSON(false);
            SetReward(-1f);
            Debug.Log("muerte");
            EndEpisode();
        }
    }



}

[System.Serializable]
public class playerData 
{
    public int total_times_won;
    public List<int> times_won;
    public List<Vector2> local_player_velocity;  // velocityplayer - velocityenemy
    public List<Vector2> local_enemy_velocity;  // enemy_velocity - player_velocity
    public List<Vector2> direction_player;
    public List<Vector2> local_position_player; // player - enemy position 
    public List<Vector2> local_position_enemy; // enemy - player position

}
