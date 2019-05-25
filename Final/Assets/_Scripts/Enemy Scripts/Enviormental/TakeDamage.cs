using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakeDamage : MonoBehaviour
{
    public GameObject EnemyObject;
    public GameObject DamageText;
    public GameObject[] BloodDecals;
    public GameObject SplatLocation;
    private int layermask = 12;
    private bool hit = false;
    private bool alive = true;

    private float timer = 0;
    float damage = 0;
    private void Start()
    {
        layermask = ~layermask;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            RaycastHit rayHIT;

             if (Physics.Raycast(SplatLocation.transform.position, Vector3.down, out rayHIT, Mathf.Infinity, layermask))
            {
                Debug.Log(rayHIT.point);
                Vector3 location = new Vector3(rayHIT.point.x, rayHIT.point.y + 0.01f, rayHIT.point.z);
                int decal = Random.Range(0, 6);
                Instantiate(BloodDecals[decal], location, BloodDecals[decal].transform.rotation);
            }

            hit = true;
            damage = collision.gameObject.GetComponent<Projectile>().getBulletDamage();
            if (this.gameObject.tag == "HeadShot")
            {
                damage *= 1.17f;
                DamageText.GetComponent<Text>().color = Color.yellow;
            }
            else DamageText.GetComponent<Text>().color = Color.white;

            EnemyObject.GetComponent<Enemy_Base>().TakeDamage(damage);
        }
        DamageText.GetComponent<Text>().text = damage.ToString();





    }


    private void Update()
    {
        Debug.DrawRay(SplatLocation.transform.position, Vector3.down);
        alive = EnemyObject.GetComponent<Enemy_Base>().EnemyHealth.m_Alive;

        if (hit)
        {
            timer += Time.deltaTime;
            if (timer > 0.75f)
            {
                DamageText.GetComponent<Text>().text = "";
                timer = 0;
                hit = false;
            }
        }
        if (!alive)
        {
            DamageText.GetComponent<Text>().color = Color.red;
            DamageText.GetComponent<Text>().text = "Dead";

            Destroy(this, 0.75f);
        }
    }
}
