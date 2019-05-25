using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State_Ragdoll : IState
{
    private GameObject Owner;
    private NavMeshAgent agent;
    private Rigidbody[] rbs;
    private Animator animController;

    public State_Ragdoll(GameObject owner)
    {
        Owner = owner;
        animController = Owner.GetComponent<Animator>();
        agent = Owner.GetComponent<NavMeshAgent>();
    }

    public void Enter()
    {
        rbs = Owner.GetComponentsInChildren<Rigidbody>();
    }

    public void Exit()
    {

    }

    public void Run()
    {
        if(agent)
        agent.isStopped = true;

        agent.enabled = false;


        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddExplosionForce(2, (Vector3.right), 2);
        }
        animController.enabled = false;

    }
}
