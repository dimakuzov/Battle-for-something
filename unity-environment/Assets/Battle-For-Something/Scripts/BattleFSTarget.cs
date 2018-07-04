using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFSTarget : MonoBehaviour {

    public BattleField area;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider othen)
    {
        if(othen.gameObject.tag == "yellowGoal")
        {
            area.GoalTouched("red");
        }
        if (othen.gameObject.tag == "redGoal")
        {
            area.GoalTouched("yellow");
        }
    }

}
