using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public Shotgun()
    {
        m_clipSize = 30;
        m_ammoPool = 0;

        m_bulletDmg = 5.5f;
        m_bulletRange = 10f;
        m_bulletSpeed = 500;
        m_shotRecoil = 0;
        m_rateOfFire = 1f;
        m_Automatic = false;

        m_WeaponID = (int)Weapons.Shotgun;

        Debug.Log("Shotgun Constructor Called");
    }

    protected override void FireBullet()
    {
        GetComponent<Animator>().SetTrigger("Shot");
        VFX.GetComponent<ParticleSystem>().Play();
        SFX.pitch = Random.Range(0.8f, 1f);
        SFX.PlayOneShot(shotSFX);
        GameObject[] Bullets = new GameObject[15];
        for (int i = 0; i < 15; i++)
        {
            Vector3 direction = m_shotPoint.transform.position + Random.insideUnitSphere * 0.5f;
            GameObject bullet = Instantiate(m_projectile, direction, m_shotPoint.transform.rotation);
            bullet.GetComponent<Projectile>().setBulletDamage(m_bulletDmg);
            bullet.GetComponent<Projectile>().setBulletRange(m_bulletRange);

            Bullets[i] = bullet; 
            
            //bullet.GetComponent<Rigidbody>().velocity = transform.forward * m_bulletSpeed;
            m_curClipAmmo--;
        }
        for(int i = 0; i <15; i++)
            Bullets[i].GetComponent<Rigidbody>().velocity = transform.forward * m_bulletSpeed;
    }

    protected override void ReloadHandler()
    {
        if (Input.GetKeyDown("r") && m_curClipAmmo < m_clipSize)
        {
            SFX.PlayOneShot(reloadSFX);
            GetComponent<Animator>().SetBool("isReloading", true);
            for (int i = m_curClipAmmo; i < m_clipSize; i++)
            {
                if (Player.GetComponent<FPS_Inventory>().GetWeaponAmmo(m_WeaponID) > 0)
                {
                    m_curClipAmmo++;
                    Player.GetComponent<FPS_Inventory>().ModifyWeaponAmmo(m_WeaponID, "sub", 1);
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
