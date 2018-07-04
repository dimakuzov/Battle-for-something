using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFSAcademy : Academy {

    [Header("For worker agent")]
    public float agentWorkerRunSpeed;
    public float angelCrashWorker;
    public float centerOfMass;

    [Header("For aggressor agent")]
    public float agentRunSpeed;
    public float torqueAggressor;
    public float angelCrashAggressor;
    public float centerOfMassAggres;

    [Header("For all agent")]
    public float timeSwitchEnable;
    public float radiusTarget;
    public bool randomForLearn;
}
