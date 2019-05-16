using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingOrb : MonoBehaviour
{

    [Header("FIRING")]
    [Tooltip("Bullet Instantiation Point")]
    public GameObject shotLocation;
    [Tooltip("Projectile Prefab to be fired")]
    public GameObject turretProjectile;
    [Tooltip("Projectile Speed")]
    [Range(5f, 50f)]
    public float projectileSpeed = 15f;
    [Tooltip("Projectile Speed")]
    [Range(0f, 200f)]
    public float projectileDamage = 20f;
    [Header("VFX")]
    [Tooltip("Visual Effect")]
    public GameObject VFX;

    [Space(10)]
    [Header("Targeting")]
    [SerializeField]
    private bool canFire = false;


    public void setFireStatus(bool status)
    {
        canFire = status;
    }

    public void FireProjectile()
    {
        if (canFire == true)
        {
            VFX.GetComponent<ParticleSystem>().Play();
            Vector3 direction = shotLocation.transform.position;
            GameObject bullet;
            bullet = Instantiate(turretProjectile, direction, shotLocation.transform.rotation);
            bullet.GetComponent<EnemyProjectile>().SetProjectileDamage(projectileDamage);
            bullet.GetComponent<Rigidbody>().velocity = shotLocation.transform.forward * projectileSpeed;
        }
    }
}
