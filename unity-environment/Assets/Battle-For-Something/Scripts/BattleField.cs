using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStateBFS
{
    public AgentWorker workerScript;
    public AgentAggressor aggressorScript;
    public string team;
}

public class BattleField : MonoBehaviour {

    //BattleFSAcademy academy;
    public List<PlayerStateBFS> playerStates = new List<PlayerStateBFS>();
    public Light redTeamLight;
    public Light yellowTeamLight;
    Color redLightColor;
    Color yellowLightColor;

    public GameObject ball;
    Vector3 ballStartPos;
    
    void Start () {
        redLightColor = redTeamLight.color;
        yellowLightColor = yellowTeamLight.color;
        //academy = FindObjectOfType<BattleFSAcademy>();
        ballStartPos = ball.transform.position;
    }
	
	void Update () {
	}

    public void GoalTouched(string team)
    {
        if (team == "red")
        {
            foreach (PlayerStateBFS ps in playerStates)
            {
                if (ps.workerScript)
                    ps.workerScript.whoWin = "red";
                else
                    ps.aggressorScript.whoWin = "red";
            }
            StartCoroutine(GoalIndicator("redTeamWin"));
            Debug.Log("redTeamWin");
        }
        else
        {
            foreach (PlayerStateBFS ps in playerStates)
            {
                if (ps.workerScript)
                    ps.workerScript.whoWin = "yellow";
                else
                    ps.aggressorScript.whoWin = "yellow";
            }
            StartCoroutine(GoalIndicator("yellowTeamWin"));
            Debug.Log("yellowTeamWin");
        }
    }

    public IEnumerator GoalIndicator(string teamWin)
    {
        if(teamWin == "redTeamWin")
        {
            yellowTeamLight.color = redLightColor;
            yield return new WaitForSeconds(0.8f);
            yellowTeamLight.color = yellowLightColor;
        }
        else
        {
            redTeamLight.color = yellowLightColor;
            yield return new WaitForSeconds(0.8f);
            redTeamLight.color = redLightColor;
        }
        yield return null;
    }

    public void ResetBall()
    {
        Rigidbody ballRB = ball.GetComponent<Rigidbody>();
        ball.transform.position = new Vector3(ballStartPos.x, 
                    ballStartPos.y, ballStartPos.z + Random.Range(-0.7f, 0.7f));
        ballRB.velocity = Vector3.zero;
        ballRB.angularVelocity = Vector3.zero;
    }
}