using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{
    public Crossbow()
    {
        m_clipSize = 6;
        m_ammoPool = 0;

        m_bulletDmg = 55.5f;
        m_bulletRange = 40f;
        m_bulletSpeed = 150f;
        m_shotRecoil = 0;
        m_rateOfFire = 0.75f;
        m_Automatic = false;

        m_WeaponID = (int)Weapons.Crossbow;

        Debug.Log("Pistol Constructor Called");
    }

}
