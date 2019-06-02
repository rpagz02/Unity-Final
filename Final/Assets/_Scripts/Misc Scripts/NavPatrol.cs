using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavPatrol : MonoBehaviour
{
    // Place this script on cinemachine characters that you want to animate 
    // in script 

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    [SerializeField]
    private bool GaveSignal = false;
    private float signalTime = 10;
    private float timer = 0;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        GaveSignal = false;
        // GotoNextPoint();        
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;
       // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }


    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=10)
        {
            GaveSignal = true;
        }

        if (GaveSignal == true)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
        }

    }

    public void GiveSignal()
    {
        //GaveSignal = true;
    }
}
