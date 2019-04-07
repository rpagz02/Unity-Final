using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public Shotgun()
    {
        m_clipSize = 4;
        m_ammoPool = 0;

        m_bulletDmg = 5.5f;
        m_bulletRange = 10f;
        m_bulletSpeed = 200f;
        m_shotRecoil = 0;
        m_rateOfFire = 1f;
        m_Automatic = false;

        m_WeaponID = (int)Weapons.Shotgun;

        Debug.Log("Shotgun Constructor Called");
    }
}
