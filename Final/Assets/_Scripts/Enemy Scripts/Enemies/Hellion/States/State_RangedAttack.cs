using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State_RangedAttack : IState
{
    private GameObject Owner;
    private Animator animController;
    private NavMeshAgent agent;
    private GameObject FireballPrefab;
    private Transform shoPos;


    public State_RangedAttack(GameObject owner)
    {
        Owner = owner;
        animController = Owner.GetComponent<Animator>();
        agent = Owner.GetComponent<NavMeshAgent>();
    }

    public void Enter()
    {
        agent.isStopped = true;
        agent.ResetPath();
        animController.SetTrigger("Ranged");
    }

    public void Exit()
    {
    }

    public void Run()
    {
    }
}
