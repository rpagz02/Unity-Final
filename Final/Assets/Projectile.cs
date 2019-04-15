using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   private float m_projectileDmg = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    public void setBulletDamage(float dmg) { m_projectileDmg = dmg; }
    public float getBulletDamage() { return m_projectileDmg; }
}
