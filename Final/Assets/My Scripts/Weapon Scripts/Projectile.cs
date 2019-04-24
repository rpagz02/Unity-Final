using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   private float m_projectileDmg = 0;
    private float baseDamage;
    private float range_timer = 0;
    private float range = 0;

    private float bulletWeight;
    Vector3 bulletDrop_Gravity = Vector3.zero;

    private void Start()
    {
        bulletDrop_Gravity = new Vector3(0, -2, 0);
    }

    void Update()
    {
        // Self destruct the projectile after a 2 second delay
        Destroy(this.gameObject, range);
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


        this.GetComponent<Rigidbody>().AddForce(bulletDrop_Gravity);

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
    public float getBulletRange() { return range; }
    public void setBulletRange(float _range) { range = _range; }
}
