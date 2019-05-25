using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelleCollider : MonoBehaviour
{
    public GameObject MyBody;
    private float myDamage = 0;
    private bool damaged = false;


    // Set the damage from the unique enemy script in the start function.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (damaged == false)
            {
                other.gameObject.GetComponent<FPS_Player>().DamagePlayer(myDamage);
                damaged = true;
            }
        }
    }

    private void OnTriggerExit()
    {
        damaged = false;
    }

    public void SetMeleeDamage(float damage)
    {
        myDamage = damage;
    }
}
