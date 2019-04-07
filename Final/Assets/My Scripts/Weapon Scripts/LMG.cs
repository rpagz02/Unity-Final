using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMG : Weapon
{
    public LMG()
    {
        m_clipSize = 30;
        m_ammoPool = 0;

        m_bulletDmg = 3.5f;
        m_bulletRange = 18;
        m_bulletSpeed = 120;
        m_shotRecoil = 0;
        m_rateOfFire = 1f;
        m_Automatic = true;

        m_WeaponID = (int)Weapons.LMG;

        Debug.Log("LMG Constructor Called");
    }
}
