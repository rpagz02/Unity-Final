using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

    private void Start()
    {
        gunAmmo.m_clipSize = 8;
        gunAmmo.m_ammoPool = 0;
        gunBullet.m_bulletDmg = 25.5f;
        gunBullet.m_bulletRange = 2f;
        gunBullet.m_bulletSpeed = 100f;
        gunBullet.m_shotRecoil = 0;
        gunBullet.m_rateOfFire = 0.2f;
        gunBullet.m_Automatic = false;

        gunAmmo.m_WeaponID = (int)Weapons.Pistol;

        toggle = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        SFX = GetComponent<AudioSource>();
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
