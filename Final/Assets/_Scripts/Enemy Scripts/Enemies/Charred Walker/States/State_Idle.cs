using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Idle : IState
{
    private GameObject Owner;

    // Constructor
    // --
    // Assign the Owner to the Object that called it
    public State_Idle(GameObject owner)
    {
        this.Owner = owner;
    }


    public void Enter()
    {
        if (Owner != null)
        {
            Owner.GetComponent<Animator>().SetBool("Idle", true);
        }
    }
    public void Exit()
    {
        if (Owner != null)
        {
            Owner.GetComponent<Animator>().SetBool("Idle", false);
        }
    }

    public void Run()
    {

    }


}
