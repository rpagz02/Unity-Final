using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{

    private void Start()
    {
        gunAmmo.m_clipSize = 6;
        gunAmmo.m_ammoPool = 0;

        gunBullet.m_bulletDmg = 55.5f;
        gunBullet.m_bulletRange = 40f;
        gunBullet.m_bulletSpeed = 150f;
        gunBullet.m_shotRecoil = 0;
        gunBullet.m_rateOfFire = 0.75f;
        gunBullet.m_Automatic = false;

        gunAmmo.m_WeaponID = (int)Weapons.Crossbow;

        toggle = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        SFX = GetComponent<AudioSource>();
    }
}
