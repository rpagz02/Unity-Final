using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LMG : Weapon
{
    private void Start()
    {
        gunAmmo.m_clipSize = 30;
        gunAmmo.m_ammoPool = 0;

        gunBullet.m_bulletDmg = 3.5f;
        gunBullet.m_bulletRange = 18;
        gunBullet.m_bulletSpeed = 120;
        gunBullet.m_shotRecoil = 0;
        gunBullet.m_rateOfFire = 1f;
        gunBullet.m_Automatic = true;

        gunAmmo.m_WeaponID = (int)Weapons.LMG;

        toggle = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        SFX = GetComponent<AudioSource>();
    }
}
