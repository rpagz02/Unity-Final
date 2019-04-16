﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   private float m_projectileDmg = 0;
    private float baseDamage;
    private float range_timer = 0;

    void Update()
    {
        // Self destruct the projectile after a 2 second delay
        Destroy(this.gameObject, 2.0f);
        // Testing out the damage falloff logic here
        range_timer += Time.deltaTime;
        if (range_timer <= 0.18f)
            m_projectileDmg = baseDamage;
        else if (range_timer <= 0.45f)
            m_projectileDmg = baseDamage * 0.66f;
        else if (range_timer <= 0.6f)
            m_projectileDmg = baseDamage * 0.33f;
        else if (range_timer <= 0.65f)
            m_projectileDmg = baseDamage * 0.15f;
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the Bullet if it hits anything
        // with the exception of the targets(they have special logic behind them)

        if(collision.gameObject.layer != 8)
        Destroy(this.gameObject);
    }

    public void setBulletDamage(float dmg) { m_projectileDmg = dmg; baseDamage = dmg; }
    public float getBulletDamage() { return m_projectileDmg; }
}
