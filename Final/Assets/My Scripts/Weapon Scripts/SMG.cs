using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Weapon
{

    private void Start()
    {
        gunAmmo.m_clipSize = 30;
        gunAmmo.m_ammoPool = 0;

        gunBullet.m_bulletDmg = 3.5f;
        gunBullet.m_bulletRange = 18;
        gunBullet.m_bulletSpeed = 120;
        gunBullet.m_shotRecoil = 0;
        gunBullet.m_rateOfFire = 0.12f;
        gunBullet.m_Automatic = true;

        gunAmmo.m_WeaponID = (int)Weapons.SMG;

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
