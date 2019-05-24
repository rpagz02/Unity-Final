using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelleCollider : MonoBehaviour
{
    public GameObject MyBody;
    private float myDamage = 0;


    // Set the damage from the unique enemy script in the start function.
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<FPS_Player>().DamagePlayer(myDamage);
        }
    }

    public void SetMeleeDamage(float damage)
    {
        myDamage = damage;
    }
}
