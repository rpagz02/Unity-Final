using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Weapon
{
    public SMG()
    {
        m_clipSize = 30;
        m_ammoPool = 0;

        m_bulletDmg = 3.5f;
        m_bulletRange = 18;
        m_bulletSpeed = 120;
        m_shotRecoil = 0;
        m_rateOfFire = 1f;
        m_Automatic = true;

        m_WeaponID = (int)Weapons.SMG;
        Debug.Log("SMG Constructor Called");

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

    protected override void AttackHandler()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Automatic Fire Called");
            if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Recharge") &&
                           !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shot") &&
                           !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                m_Timer += Time.deltaTime;
                if (m_Timer > 0.1f)
                {
                    FireBullet();
                    m_Timer = 0;
                }
            }
        }
    }
}
