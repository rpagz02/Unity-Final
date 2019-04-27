using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    public Rifle()
    {
        m_clipSize = 50;
        m_ammoPool = 0;

        m_bulletDmg = 6f;
        m_bulletRange = 18;
        m_bulletSpeed = 500;
        m_shotRecoil = 0;
        m_rateOfFire = 0.2f;
        m_Automatic = false;

        m_WeaponID = (int)Weapons.Rifle;

        Debug.Log("Rifle Constructor Called");
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
