using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


///////////////////////////////////////////////////////////////////////////
/// These are utility methods to be called by the different Enemy        //
///    classes. Done to free up some space on the Enemy Scripts and      //
///    make them more generic.                                           //
///////////////////////////////////////////////////////////////////////////

public static class EAttackUtils
{
    
    public static void AI_MeleeAttack(GameObject thisObj, GameObject[] hitColliders)
    {
        NavMeshAgent agent = thisObj.GetComponent<NavMeshAgent>();
        Animator animController = thisObj.GetComponent<Animator>();

        agent.isStopped = true;
        agent.ResetPath();
    }

    public static void AI_RangedAttack()
    {

    }

    public static void AI_SpawnEnemy()
    {

    }

}
