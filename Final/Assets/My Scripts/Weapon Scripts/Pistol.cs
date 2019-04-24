using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

    public Pistol()
    {
        m_clipSize = 7;
        m_ammoPool = 0;
        m_bulletDmg = 25.5f;
        m_bulletRange = 2f;
        m_bulletSpeed = 100f;
        m_shotRecoil = 0;
        m_rateOfFire = 0.2f;
        m_Automatic = false;

        m_WeaponID = (int)Weapons.Pistol;

        Debug.Log("Pistol Constructor Called");
    }

    protected override void ReloadHandler()
    {
        if (Input.GetKeyDown("r") && m_curClipAmmo < m_clipSize)
        {
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
