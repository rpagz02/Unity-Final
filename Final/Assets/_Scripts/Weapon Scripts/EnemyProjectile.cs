using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private float damageAmnt = 0;
    private bool damaged = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, -2, 0)); // bullet drop gravity

    }

    private void OnCollisionEnter(Collision collision)
    {
            Destroy(this.gameObject);
        if(collision.gameObject.layer == 20) // Player Layer
        {
            if(!damaged)
            collision.gameObject.GetComponent<FPS_Player>().DamagePlayer(damageAmnt);
            damaged = true;
        }
    }

    public void SetProjectileDamage(float damage)
    {
        damageAmnt = damage;
    }
    public float GetProjectileDamage()
    {
        return damageAmnt;
    }
}
