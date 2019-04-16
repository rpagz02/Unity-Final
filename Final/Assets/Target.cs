using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    public GameObject dmgText;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8) // projectile layer
        {
            dmgText.GetComponent<Text>().text = collision.gameObject.GetComponent<Projectile>().getBulletDamage().ToString();
            Destroy(collision.gameObject);
            
        }
    }
}
