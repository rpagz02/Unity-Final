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
        m_bulletRange = 30f;
        m_bulletSpeed = 100f;
        m_shotRecoil = 0;
        m_rateOfFire = 0.2f;
        m_Automatic = false;

        m_WeaponID = (int)Weapons.Pistol;

        Debug.Log("Pistol Constructor Called");
    }

}
