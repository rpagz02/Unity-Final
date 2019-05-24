using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State_Attack : IState
{
    private GameObject Owner;
    private Animator animController;
    private NavMeshAgent agent;


    public State_Attack(GameObject owner)
    {
        Owner = owner;
        animController = Owner.GetComponent<Animator>();
        agent = Owner.GetComponent<NavMeshAgent>();
    }

    public void Enter()
    {
        agent.isStopped = true;
        agent.ResetPath();
    }

    public void Exit()
    {
        animController.SetBool("Attack", false);
    }

    public void Run()
    {
        animController.SetBool("Attack", true);
    }
}
