using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class State_Chase : IState
{
    private GameObject Owner;
    private GameObject Player;
    private Animator animController;
    private NavMeshAgent agent;

    private float timer;


    public State_Chase(GameObject owner)
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        this.Owner = owner;
        animController = Owner.GetComponent<Animator>();
        agent = Owner.GetComponent<NavMeshAgent>();
    }

    public void Enter()
    {
        animController.SetBool("Walk", true);
    }

    public void Exit()
    {
        animController.SetBool("Walk", false);
    }

    public void Run()
    {
        if (Player != null)
        {
            ETargetingUtils.AI_Chase(Owner, Player);
            LookAtPlayer();
        }
        else
            Debug.Log("Player is Null is Chase Method");
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
