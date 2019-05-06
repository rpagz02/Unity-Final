using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   private float m_projectileDmg = 0;
    private float baseDamage;
    private float range_timer = 0;
    private float range = 0;
    private float timer = 0;
    public GameObject ImpactDecal;


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

        if (range_timer <= 0.1f)
            m_projectileDmg = baseDamage;
        else if (range_timer <= 0.2f)
            m_projectileDmg = baseDamage * 0.8f;
        else if (range_timer <= 0.3f)
            m_projectileDmg = baseDamage * 0.7f;
        else if (range_timer <= 0.4f)
            m_projectileDmg = baseDamage * 0.65f;
        else if (range_timer <= 0.5f)
            m_projectileDmg = baseDamage * 0.46f;
        else if (range_timer <= 0.6f)
            m_projectileDmg = baseDamage * 0.35f;
        else if (range_timer <= 0.65f)
            m_projectileDmg = baseDamage * 0.15f;


        this.GetComponent<Rigidbody>().AddForce(bulletDrop_Gravity);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 8 && collision.gameObject.tag != "Player")
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.LookRotation(contact.normal);
            Instantiate(ImpactDecal, this.transform.position, rot);
            Destroy(this.gameObject);

        }
    }
    public void setBulletDamage(float dmg) { m_projectileDmg = dmg; baseDamage = dmg; }
    public float getBulletDamage() { return m_projectileDmg; }
    public float getBulletRange() { return range; }
    public void setBulletRange(float _range) { range = _range; }


}
