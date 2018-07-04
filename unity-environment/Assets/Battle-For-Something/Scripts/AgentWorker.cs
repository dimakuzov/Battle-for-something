using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWorker : Agent {
    
    public enum Team
    {
        red, yellow
    }

    [HideInInspector]
    public Rigidbody agentRB;
    BattleFSAcademy academy;
    public BattleField area;
    public Team team;
    public Material teamMat;
    public Material bodyMat;
    Color teamColor;
    Color bodyColor;
    Material agentTeamMat;

    public Transform target;
    float currentDisTargetToGoal;
    float currentDisAgentToTarget;
    float lastDisTargetToGoal;
    float lastDisAgentToTarget;
    [HideInInspector]
    public bool crashAgent = false;
    public BattleField bField;
    string tagAgent;
    [HideInInspector]
    public string whoWin;
    Vector3 startPos;
    Quaternion startRot;
    public Transform GoalRed;
    public Transform GoalYellow;
    Transform needGoal;
    Vector2 agentVelocity;
    float radiusTarget;
    RayPerception rayPer;
    float punishForAct;
    float angleForce;
    bool forCollisionsuccess = false;
    float reward = 0.0f;

    public override void InitializeAgent()
    {
        agentRB = GetComponent<Rigidbody>();
        academy = FindObjectOfType<BattleFSAcademy>();
        PlayerStateBFS playerState = new PlayerStateBFS();
        playerState.workerScript = this;
        if (gameObject.tag == "redAgent")
        {
            playerState.team = "red";
            team = Team.red;
            needGoal = GoalYellow;
        }
        else
        {
            playerState.team = "yellow";
            team = Team.yellow;
            needGoal = GoalRed;
        }
        area.playerStates.Add(playerState);
        tagAgent = gameObject.tag;

        teamColor = teamMat.color;
        bodyColor = bodyMat.color;
        Renderer rend = gameObject.GetComponentInChildren<Renderer>() as Renderer;
        agentTeamMat = rend.material;

        startPos = transform.position;
        startRot = transform.rotation;
        whoWin = "coming";
        agentRB.centerOfMass = new Vector3(0, academy.centerOfMass, 0);
        radiusTarget = academy.radiusTarget;
        rayPer = GetComponent<RayPerception>();
    }

    public override void CollectObservations()
    {
        Vector2 posTarget = new Vector2(target.position.x - area.transform.position.x,
                                        target.position.z - area.transform.position.z);
        Vector2 posAgent = new Vector2(transform.position.x - area.transform.position.x,
                                        transform.position.z - area.transform.position.z);
        Vector2 posGoal = new Vector2(needGoal.position.x - area.transform.position.x,
                                        needGoal.position.z - area.transform.position.z);
        currentDisTargetToGoal = Vector2.Distance(posTarget, posGoal);
        currentDisAgentToTarget = Vector2.Distance(posAgent, posTarget);
        agentVelocity = new Vector2(agentRB.velocity.x, agentRB.velocity.z);

        AddVectorObs(Mathf.Sin(angleForce * Mathf.PI));
        AddVectorObs(Mathf.Cos(angleForce * Mathf.PI));

        AddVectorObs(posTarget);
        AddVectorObs(posAgent);
        AddVectorObs(posGoal);
        AddVectorObs(agentVelocity);
        AddVectorObs(currentDisTargetToGoal);
        AddVectorObs(currentDisAgentToTarget);
        float alive = crashAgent == false ? 1f : 0;
        AddVectorObs(alive);

        AddVectorObs(radiusTarget);
        AddVectorObs(target.GetComponent<Rigidbody>().velocity.x);
        AddVectorObs(target.GetComponent<Rigidbody>().velocity.z);
        /*
        float rayDistance = 4f;
        float[] rayAngles = { 0f, 45f, 90f, 135f, 180f, 225f, 270f, 315f};
        string[] detectableObjects;
        if (team == Team.red)
        {
            detectableObjects = new string[] { "target",
                "wall", "redGoal", "yellowGoal"};//, "redAgent", "yellowAgent", "deadAgent"};
        }
        else
        {
            detectableObjects = new string[] { "target",
                "wall", "yellowGoal", "redGoal"};//, "yellowAgent", "redAgent", "deadAgent" };
        }
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        */
    }
    
    public void MoveAgent(float[] act)
    {
        float an = academy.angelCrashWorker;
        Vector3 workerAngle = transform.eulerAngles;
        punishForAct = act[0];
        angleForce = Mathf.Clamp(act[0], -1, 1);
        //float runSpeed = academy.agentWorkerRunSpeed * Mathf.Clamp(act[1], -1, 1);
        
        if (workerAngle.x < an || workerAngle.x > (360f - an))
        {
            if (workerAngle.z < an || workerAngle.z > (360f - an))
            {
                agentRB.AddForce(new Vector3(Mathf.Sin(angleForce * Mathf.PI),
                                0, Mathf.Cos(angleForce * Mathf.PI))
                                * academy.agentWorkerRunSpeed, ForceMode.VelocityChange);
                                //* runSpeed, ForceMode.VelocityChange);
                AgentIsAlive(true);
            }
            else
                AgentIsAlive(false);
        }
        else
            AgentIsAlive(false);

        //Debug.Log("angleForce: " + angleForce);
        //Debug.Log("runSpeed: " + runSpeed);
        //Debug.Log("act[0]: " + act[0]);
        //Debug.Log("act[1]: " + act[1]);
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
        //Debug.Log("Action complite");
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        if (whoWin != "coming" && crashAgent == false)
        {
            if (whoWin == "red" && gameObject.tag == "redAgent")
            {
                AddReward(1.0f);
                Done();
            }
            else if (whoWin == "yellow" && gameObject.tag == "yellowAgent")
            {
                AddReward(1.0f);
                Done();
            }
            else
            {
                AddReward(-1.0f);
                Done();
            }
            whoWin = "coming";
        }
        else if (whoWin != "coming" && crashAgent == true)
        {
            Done();
            whoWin = "coming";
        }
        if (crashAgent == false)
        {
            if (currentDisTargetToGoal >= lastDisTargetToGoal)
            {
                AddReward(-0.4f);
                forCollisionsuccess = false;
            }
            else {
                AddReward(0.4f);
                forCollisionsuccess = true;
            }

            if (currentDisAgentToTarget > (lastDisAgentToTarget + 0.0013f))
                AddReward(-0.1f);//   -0.1f

            else if (currentDisAgentToTarget <= (lastDisAgentToTarget - 0.0013f))
                AddReward(0.1f);

            else
                AddReward(-0.0002f);
        }

        AddReward(punishForAct > 1.0f ? -1.0f : 0.0f);
        AddReward(punishForAct < -1.0f ? -1.0f : 0.0f);
        AddReward(reward);

        reward = 0;
        lastDisAgentToTarget = currentDisAgentToTarget;
        lastDisTargetToGoal = currentDisTargetToGoal;
        MoveAgent(vectorAction);
    }
    
    IEnumerator VisualEnableAgent(bool switchOn)
    {
        if(switchOn)
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
    
    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "target" && forCollisionsuccess)
        {
            reward = 0.7f;
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag != gameObject.tag &&
           col.gameObject.tag != "yellowGoal" &&
           col.gameObject.tag != "redGoal" &&
           //col.gameObject.GetComponent<AgentAggressor>() &&
           gameObject.tag != "deadAgent")
        {
            agentRB.velocity = Vector3.up * 4.0f;
            agentRB.maxAngularVelocity = 1000.0f;
            agentRB.angularVelocity = Vector3.right * 90.0f;
            AgentAggressor ag = col.gameObject.GetComponentInParent(typeof(AgentAggressor)) as AgentAggressor;
            ag.rewardThisAgent = 1.0f;
        }
    }
    
    public override void AgentReset()
    {
        if (academy.randomForLearn)
        {
            float x = area.transform.position.x;
            if (Mathf.FloorToInt(Random.Range(-0.0f, 1.99f)) == 0)
            {
                x -= 1.0f;
                tag = "yellowAgent";
                team = Team.yellow;
                needGoal = GoalRed;
            }
            else
            {
                x += 1.0f;
                tag = "redAgent";
                team = Team.red;
                needGoal = GoalYellow;
            }
            startPos = new Vector3(x, 0.0f, Random.Range(-0.4f, 0.4f) + area.transform.position.z);
        }

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