using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState CurrState;
    private IState PrevState;



    public void ChangeState(IState newState)
    {
        if(CurrState != null)
        this.CurrState.Exit();
        
        this.PrevState = this.CurrState;

        this.CurrState = newState;
        this.CurrState.Enter();
    }

    public void RunCurrentState()
    {
        var RunningState = this.CurrState;
        if(RunningState != null)
        this.CurrState.Run();
    }

    public void BackToPreviousState()
    {
        this.CurrState.Exit();
        this.CurrState = PrevState;
        this.CurrState.Enter();
    }

}
