using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAggressor : Agent {


    public enum Team
    {
        red, yellow
    }

    [HideInInspector]
    public Rigidbody agentRB;
    BattleFSAcademy academy;
    public BattleField area;
    public Team team;
    RayPerception rayPer;
    public Material teamMat;
    public Material bodyMat;
    Color teamColor;
    Color bodyColor;
    Material agentTeamMat;
    string tagAgent;

    public float rewardThisAgent = 0;
    float rewardSmall = 0;
    [HideInInspector]
    public bool crashAgent = false;
    [HideInInspector]
    public string whoWin;
    Vector3 startPos;
    Quaternion startRot;

    public override void InitializeAgent()
    {
        agentRB = GetComponent<Rigidbody>();
        academy = FindObjectOfType<BattleFSAcademy>();
        PlayerStateBFS playerState = new PlayerStateBFS();
        playerState.aggressorScript = this;
        if (gameObject.tag == "redAgent")
        {
            playerState.team = "red";
            team = Team.red;
        }
        else
        {
            playerState.team = "yellow";
            team = Team.yellow;
        }
        area.playerStates.Add(playerState);
        rayPer = GetComponent<RayPerception>();
        tagAgent = gameObject.tag;
        whoWin = "coming";
        startPos = transform.position;
        startRot = transform.rotation;

        teamColor = teamMat.color;
        bodyColor = bodyMat.color;
        Renderer rend = gameObject.GetComponentInChildren<Renderer>() as Renderer;
        agentTeamMat = rend.material;
        agentRB.centerOfMass = new Vector3(0, academy.centerOfMassAggres, 0);
    }

    public override void CollectObservations()
    {
        float rayDistance = 4f;
        float[] rayAngles = { 0f, 45f, 90f, 135f, 180f, 110f, 70f, 270f, 315f, 225f, 80f, 100f};
        string[] detectableObjects;
        if (team == Team.red)
        {
            detectableObjects = new string[] { "target",
                "wall", "redAgent", "yellowAgent", "deadAgent"};
        }
        else
        {
            detectableObjects = new string[] { "target",
                "wall", "yellowAgent", "redAgent", "deadAgent" };
        }
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        float alive = crashAgent == false ? 1f : 0;
        AddVectorObs(alive);
        Vector2 agentVelocity = new Vector2(agentRB.velocity.x, agentRB.velocity.z);
        Vector2 posAgent = new Vector2(transform.position.x - area.transform.position.x,
                                        transform.position.z - area.transform.position.z);
        AddVectorObs(posAgent);
        AddVectorObs(agentVelocity);
    }

    public void MoveAgent(float[] act)
    {
        float an = academy.angelCrashAggressor;
        Vector3 workerAngle = transform.eulerAngles;

        if (workerAngle.x < an || workerAngle.x > (360f - an))
        {
            if (workerAngle.z < an || workerAngle.z > (360f - an))
            {
                Vector3 dirToGo = Vector3.zero;
                Vector3 rotateDir = Vector3.zero;
                int action = Mathf.FloorToInt(act[0]);

                switch (action)
                {
                    case 0:
                        rotateDir = transform.up * 1f;
                        break;
                    case 1:
                        rotateDir = transform.up * -1f;
                        break;
                    case 2:
                        dirToGo = transform.forward * 0.05f;
                        break;
                    case 3:
                        dirToGo = transform.forward * -0.05f;
                        break;
                }
                transform.Rotate(rotateDir, Time.deltaTime * academy.torqueAggressor);
                agentRB.AddForce(dirToGo * academy.agentRunSpeed,
                                 ForceMode.VelocityChange);
                AgentIsAlive(true);
            }
            else
                AgentIsAlive(false);
        }
        else
            AgentIsAlive(false);
    }

    void AgentIsAlive(bool isAlive)
    {
        if (isAlive)
        {
            if (crashAgent == true)
            {
                StartCoroutine(VisualEnableAgent(true));
                gameObject.tag = tagAgent;
            }
            crashAgent = false;
        }
        else
        {
            if (crashAgent == false)
            {
                StartCoroutine(VisualEnableAgent(false));
                gameObject.tag = "deadAgent";
            }
            crashAgent = true;
        }
    }
    /*
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag != gameObject.tag)
        {
            if (other.gameObject.tag == "redAgent" || other.gameObject.tag == "yellowAgent")
            {
                string typeEnemy;
                if (other.gameObject.GetComponent<AgentWorker>())
                    typeEnemy = "worker";
                else
                    typeEnemy = "agressor";
                
                StartCoroutine(Stricken(other.gameObject, typeEnemy, true));
            }
        }
        else
        {
            string typeEnemy;
            if (other.gameObject.GetComponent<AgentWorker>())
                typeEnemy = "worker";
            else
                typeEnemy = "agressor";

            StartCoroutine(Stricken(other.gameObject, typeEnemy, false));
        }
    }

/*

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag != gameObject.tag)
        {
            if (other.gameObject.tag == "redAgent" || other.gameObject.tag == "yellowAgent") {
               if(other.gameObject.GetComponent<AgentWorker>())
                {
                    rewardSmall = 0.3f;
                }
            }
        }
        else
        {
            rewardSmall = -0.3f;
        }
    }

    public IEnumerator Stricken(GameObject stricken, string type, bool isEnemy)
    {
        if (type == "worker")
        {
            yield return new WaitForSeconds(0.4f);
            if (stricken.GetComponent<AgentWorker>().crashAgent == true && isEnemy == true)
                rewardThisAgent = 1.0f;
            if (stricken.GetComponent<AgentWorker>().crashAgent == true && isEnemy == false)
                rewardThisAgent = -1.0f;
        }
        else
        {
            yield return new WaitForSeconds(0.4f);
            if (stricken.GetComponent<AgentAggressor>().crashAgent == true && isEnemy == true)
                rewardThisAgent = 1.0f;
            if (stricken.GetComponent<AgentAggressor>().crashAgent == true && isEnemy == false)
                rewardThisAgent = -1.0f;
        }
        yield return null;
    }

*/

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (whoWin != "coming")
        {
            Done();
            whoWin = "coming";
        }
        AddReward(-1.0f / 3000.0f);
        AddReward(rewardThisAgent);
        //Debug.Log(rewardThisAgent);
        //AddReward(rewardSmall);
        /*
        if (rewardThisAgent != 0)
            Debug.Log("Goal: " + rewardThisAgent);
        if (rewardSmall != 0)
            Debug.Log("Small: " + rewardSmall);
        */
        rewardThisAgent = 0;
        //rewardSmall = 0;

        MoveAgent(vectorAction);
    }

    IEnumerator VisualEnableAgent(bool switchOn)
    {
        if (switchOn)
        {
            float counter = 0f;
            while (counter < academy.timeSwitchEnable)
            {

                counter += Time.deltaTime;
                agentTeamMat.color = Color.Lerp(bodyColor, teamColor,
                                     counter / academy.timeSwitchEnable);
                yield return null;
            }
            agentTeamMat.color = teamColor;
        }
        else
        {
            float counter = 0f;
            while (counter < academy.timeSwitchEnable)
            {
                counter += Time.deltaTime;
                agentTeamMat.color = Color.Lerp(teamColor, bodyColor,
                                     counter / academy.timeSwitchEnable);
                yield return null;
            }
            agentTeamMat.color = bodyColor;
        }
    }

    public override void AgentReset()
    {
        transform.position = startPos;
        transform.rotation = startRot;
        agentRB.velocity = Vector3.zero;
        agentRB.angularVelocity = Vector3.zero;
        area.ResetBall();
    }

    public override void AgentOnDone()
    {
        
    }
}
