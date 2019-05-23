using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State_Wander : IState
{
    private GameObject Owner;
    private Animator animController;
    private NavMeshAgent agent;
    private float wanderInterval;
    private float wanderRadius;
    private float wanderWalkSpeed;

    private float timer = 0;


   public State_Wander(GameObject owner, float _wanderInterval, float _wanderRadius, float _wanderWalkSpeed)
    {
        this.Owner = owner;
        wanderInterval = _wanderInterval;
        wanderRadius = _wanderRadius;
        wanderWalkSpeed = _wanderWalkSpeed;

        if (Owner != null)
        {
            agent = Owner.GetComponent<NavMeshAgent>();
            animController = Owner.GetComponent<Animator>();
        }

    }
    public void Enter()
    {
    }


    public void Run()
    {
        // First we handle the animations
        if (agent.velocity == Vector3.zero)
        {
            animController.SetBool("Walk", false);
        }
        else if (agent.velocity != Vector3.zero)
        {
            animController.SetBool("Walk", true);
        }
        
        // Then we handle the wandering logic
        timer += Time.deltaTime;
        if (timer >= wanderInterval)
        {
            ETargetingUtils.AI_Wander(Owner, wanderRadius);
            timer = 0;
        }
    }


    public void Exit()
    {
        animController.SetBool("Walk", false);
    }
}
