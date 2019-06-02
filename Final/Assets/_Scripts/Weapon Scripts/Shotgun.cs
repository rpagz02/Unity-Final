using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{

    private void Start()
    {
        gunAmmo.m_clipSize = 30;
        gunAmmo.m_ammoPool = 0;

        gunBullet.m_bulletDmg = 8;
        gunBullet.m_bulletRange = 2f;
        gunBullet.m_bulletSpeed = 500;
        gunBullet.m_shotRecoil = 0.005f;
        gunBullet.m_rateOfFire = 0.867f;
        gunBullet.m_Automatic = false;

        gunAmmo.m_WeaponID = (int)Weapons.Shotgun;

        toggle = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        SFX = GetComponent<AudioSource>();
    }

    protected override void FireBullet()
    {
        GetComponent<Animator>().SetTrigger("Shot");
        gunFX.VFX.GetComponent<ParticleSystem>().Play();
        SFX.pitch = Random.Range(0.8f, 1f);
        SFX.PlayOneShot(gunFX.shotSFX);
        GameObject[] Bullets = new GameObject[15];
        for (int i = 0; i < 15; i++)
        {
            GameObject bullet = Instantiate(gunBullet.m_projectile, gunBullet.m_shotPoint.transform.position, gunBullet.m_shotPoint.transform.rotation);
            bullet.GetComponent<Projectile>().setBulletDamage(gunBullet.m_bulletDmg);
            bullet.GetComponent<Projectile>().setBulletRange(gunBullet.m_bulletRange);


            Bullets[i] = bullet;
            gunAmmo.m_curClipAmmo--;
        }
        for (int i = 0; i < 15; i++)
        {
            Vector3 direction = transform.forward + Random.insideUnitSphere * gunBullet.m_shotRecoil;
            Bullets[i].GetComponent<Rigidbody>().velocity = direction * gunBullet.m_bulletSpeed;
        }
    }

    protected override void ReloadHandler()
    {
        if (Input.GetKeyDown("r") && gunAmmo.m_curClipAmmo < gunAmmo.m_clipSize)
        {
            SFX.PlayOneShot(gunFX.reloadSFX);
            GetComponent<Animator>().SetBool("isReloading", true);
            for (int i = gunAmmo.m_curClipAmmo; i < gunAmmo.m_clipSize; i++)
            {
                if (Player.GetComponent<FPS_Inventory>().GetWeaponAmmo(gunAmmo.m_WeaponID) > 0)
                {
                    gunAmmo.m_curClipAmmo++;
                    Player.GetComponent<FPS_Inventory>().ModifyWeaponAmmo(gunAmmo.m_WeaponID, "sub", 1);
                }
                else
                    return;
            }
        }
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Recharge"))
        {
            GetComponent<Animator>().SetBool("isReloading", false);
        }
    }

}
