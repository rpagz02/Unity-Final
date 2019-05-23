using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    public GameObject EnemyObject;
    public GameObject DamageText;
    private bool hit = false;

    private float timer = 0;
    float damage = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            hit = true;
            damage = collision.gameObject.GetComponent<Projectile>().getBulletDamage();
            if(this.gameObject.tag == "HeadShot")
            {
                damage *= 1.75f;
                DamageText.GetComponent<Text>().color = Color.yellow;
            }
            else DamageText.GetComponent<Text>().color = Color.white;

            EnemyObject.GetComponent<Enemy_Base>().TakeDamage(damage);
        }
        DamageText.GetComponent<Text>().text = damage.ToString();
    }


    private void Update()
    {
        if(hit)
        {
            timer += Time.deltaTime;
            if(timer > 0.75f)
            {
                DamageText.GetComponent<Text>().text = "";
                timer = 0;
                hit = false;
            }
        }
    }
}
