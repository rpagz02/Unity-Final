using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State_ChaseAtRange : IState
{
    private GameObject Owner;
    private GameObject Player;
    private Animator animController;
    private NavMeshAgent agent;

    private float timer;
    private float proximity;

    public State_ChaseAtRange(GameObject owner, float _proximity)
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        this.Owner = owner;
        animController = Owner.GetComponent<Animator>();
        agent = Owner.GetComponent<NavMeshAgent>();
        proximity = _proximity;
    }

    public void Enter()
    {
    }

    public void Exit()
    {
    }

    public void Run()
    {
        if (Player != null)
        {
            LookAtPlayer();
            if (Vector3.Distance(Owner.transform.position, Player.transform.position) <= proximity)
            {
                agent.isStopped = true;
                agent.ResetPath();
                animController.SetBool("Walk", false);
            }
            else
            {
                animController.SetBool("Walk", true);
                ETargetingUtils.AI_Chase(Owner, Player);
            }
        }
        else
            Debug.Log("Player is Null in Chase Method");
    }


    // Helper Methods
    private void LookAtPlayer()
    {
        var lookPos = Player.transform.position - Owner.transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        Owner.transform.rotation = Quaternion.Slerp(Owner.transform.rotation, rotation, Time.deltaTime);
    }

}
