using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{

    private void Start()
    {
        gunAmmo.m_clipSize = 6;
        gunAmmo.m_ammoPool = 0;

        gunBullet.m_bulletDmg = 32f;
        gunBullet.m_bulletRange = 18;
        gunBullet.m_bulletSpeed = 500;
        gunBullet.m_shotRecoil = 0.008f;
        gunBullet.m_rateOfFire = 0.4f;
        gunBullet.m_Automatic = false;

        gunAmmo.m_WeaponID = (int)Weapons.Rifle;

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
