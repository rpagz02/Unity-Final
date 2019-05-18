using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public static class ETargetingUtils
{
    ///////////////////////////////////////////////////////////////// 
    /// These are utility methods to be called by the different Enemy 
    ///    classes. Done to free up some space on the Enemy Scripts and
    ///    make them more generic.
    /////////////////////////////////////////////////////////////////

    #region AI Targeting Methods

    public static void AI_Wander(GameObject thisObj)
    {
        NavMeshAgent agent = thisObj.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        agent.ResetPath();

        Vector3 newPos = RandomNavSphere(thisObj.transform.position, 8, -1);
        thisObj.GetComponent<NavMeshAgent>().SetDestination(newPos);
    }

    public static bool AI_Target(GameObject eyePosition, GameObject thisObj, float PursuitRange)
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            #region Shoot the Debug Rays 
            RaycastHit hit;
            Debug.DrawRay(eyePosition.transform.position, thisObj.transform.TransformDirection(Vector3.forward) * PursuitRange, Color.red);
            Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(11, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.red);
            Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(22, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.red);
            Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(33, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.red);
            Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(45, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.red);
            Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(-45, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.red);
            Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(-33, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.red);
            Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(-22, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.red);
            Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(-11, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.red);
            #endregion Shooting Debug Rays

            #region Shooting Rays and Checking for Collision
            if (Physics.Raycast(eyePosition.transform.position, (thisObj.transform.TransformDirection(Vector3.forward) * PursuitRange), out hit, PursuitRange))
                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.DrawRay(eyePosition.transform.position, thisObj.transform.TransformDirection(Vector3.forward) * PursuitRange, Color.green);
                    return true;
                }

            if (Physics.Raycast(eyePosition.transform.position, ((Quaternion.AngleAxis(11, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange), out hit, PursuitRange))
                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(11, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.green);
                    return true;
                }

            if (Physics.Raycast(eyePosition.transform.position, ((Quaternion.AngleAxis(22, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange), out hit, PursuitRange))
                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(22, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.green);
                    return true;
                }

            if (Physics.Raycast(eyePosition.transform.position, ((Quaternion.AngleAxis(33, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange), out hit, PursuitRange))
                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(33, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.green);
                    return true;
                }

            if (Physics.Raycast(eyePosition.transform.position, ((Quaternion.AngleAxis(45, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange), out hit, PursuitRange))
                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(45, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.green);
                    return true;
                }

            if (Physics.Raycast(eyePosition.transform.position, ((Quaternion.AngleAxis(-45, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange), out hit, PursuitRange))
                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(-45, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.green);
                    return true;
                }

            if (Physics.Raycast(eyePosition.transform.position, ((Quaternion.AngleAxis(-33, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange), out hit, PursuitRange))
                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(-33, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.green);
                    return true;
                }

            if (Physics.Raycast(eyePosition.transform.position, ((Quaternion.AngleAxis(-22, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange), out hit, PursuitRange))
                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(-22, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.green);
                    return true;
                }

            if (Physics.Raycast(eyePosition.transform.position, ((Quaternion.AngleAxis(-11, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange), out hit, PursuitRange))
                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.DrawRay(eyePosition.transform.position, (Quaternion.AngleAxis(-11, thisObj.transform.up) * thisObj.transform.forward) * PursuitRange, Color.green);
                    return true;
                }
        }
        #endregion Shooting Rays and Checking for Collision

        return false;
    }

    public static void AI_Chase(GameObject thisObj, GameObject target)
    {
        NavMeshAgent agent = thisObj.GetComponent<NavMeshAgent>();
        agent.isStopped = false;
        agent.destination = target.transform.position;
    }

    public static void AI_STOP(GameObject thisObj)
    {
        NavMeshAgent agent = thisObj.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }

    #endregion AI Targeting Methods

    #region Helper Methods 

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
  
    #endregion Helper Methods
}
