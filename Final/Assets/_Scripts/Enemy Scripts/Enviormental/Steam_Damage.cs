using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steam_Damage : MonoBehaviour
{
    private float damageTimer = 0;
    [Header("If the damage continues, specify for how long. If not, leave this value at 0")]
    public float burnDuration = 0;
    [Header("How much DPS does the player take")]
    public float damage = 15.75f;
    [Header("How often does the player take damage while in the particle area?")]
    public float damageFrequency = 0.5f;

    private bool initialDamage = false;
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    private void Update()
    {
        if(initialDamage)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageFrequency)
            {
                Player.GetComponent<FPS_Player>().DamagePlayer(damage);
                damageTimer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (!initialDamage)
            {
                Player.GetComponent<FPS_Player>().DamagePlayer(damage);
                initialDamage = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        initialDamage = false;
    }
}
