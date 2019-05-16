using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon_Ammo
{
    [Header("The unique Weapon ID")]
    public int m_WeaponID;
    [Space(5)]
    [Header("Ammunition Variables")]
    [Tooltip("How many bullets this weapon can hold")]
    public int m_clipSize;
    [Tooltip("How many bullets are currently in the clip")]
    public int m_curClipAmmo;
    [Tooltip("Total ammount of ammo for this weapon in the inventory")]
    public int m_ammoPool;
}

[System.Serializable]
public class Weapon_Projectile
{
    [Header("Bullet Variables")]
    [Tooltip("How much damage the projectile does")]
    public float m_bulletDmg;
    [Tooltip("Velocity of the bullet")]
    public float m_bulletSpeed;
    [Tooltip("The Spread of the weapons recoil")]
    public float m_shotRecoil;
    [Tooltip("How far the bullet can go")]
    public float m_bulletRange;
    [Tooltip("How often the player can fire a bullet")]
    public float m_rateOfFire;
    [Tooltip("Is the weapon Automatic or SemiAutomatic?")]
    public bool m_Automatic;
    [Space(5)]
    [Header("Drag and Drop Objects")]
    public GameObject m_shotPoint;
    public GameObject m_projectile;
}

[System.Serializable]
public class Weapon_FX
{
    [Header("Drag and Drop SVFX")]
    public GameObject VFX;
    public AudioClip reloadSFX;
    public AudioClip shotSFX;
    public GameObject reticle_Image;
}

public abstract class Weapon : MonoBehaviour
{
    public enum Weapons { Knife, Pistol, SMG, Shotgun, Rifle, LMG, Crossbow, GrenadeLauncher, Axe, Grenade, Flashlight };
    // Local Private Variables
    protected private bool toggle;
    protected private GameObject Player;
    protected private bool isReloading;
    protected private float m_Timer;
    protected private AudioSource SFX;

    public Weapon_Ammo gunAmmo;
    public Weapon_Projectile gunBullet;
    public Weapon_FX gunFX;

    protected virtual void Update()
    {
        gunAmmo.m_ammoPool = Player.GetComponent<FPS_Inventory>().GetWeaponAmmo(gunAmmo.m_WeaponID);

        ADS();
        WeaponSprintAnim();
        AttackHandler();
        ReloadHandler();
    }

    ////////////////////////////////////////////////////////////////////////
    // Helper Methods:                                                    //
    // These cannot be overridden and will be applied to all weapons      //
    ////////////////////////////////////////////////////////////////////////
    
    #region ADS methods
    // Helper Method for ADS()
    protected void ADSToggle()
    {
        this.GetComponent<Animator>().SetBool("isADS", !toggle);
        gunFX.reticle_Image.SetActive(!toggle);
        toggle = !toggle;
    }
    // Toggle ADS when Right Mouse Held
    protected void ADS()
    {
        if (Input.GetMouseButtonDown(1))
            ADSToggle();
        else if (Input.GetMouseButtonUp(1))
            ADSToggle();
    }
    #endregion ADS methods

    protected void WeaponSprintAnim()
    {
        if (Player.GetComponent<FPS_Controller>().GetSprintStatus() == true)
            GetComponent<Animator>().SetBool("isRunning", true);
        else
            GetComponent<Animator>().SetBool("isRunning", false);

    }

    #region Projectile Methods
    protected virtual void FireBullet()
    {
        GetComponent<Animator>().SetTrigger("Shot");
        gunFX.VFX.GetComponent<ParticleSystem>().Play();
        SFX.pitch = Random.Range(0.8f, 1f);
        SFX.PlayOneShot(gunFX.shotSFX, 0.5f);

        Vector3 direction = gunBullet.m_shotPoint.transform.position + Random.insideUnitSphere * 0.1f;
        GameObject bullet = Instantiate(gunBullet.m_projectile, direction, gunBullet.m_shotPoint.transform.rotation);
        bullet.GetComponent<Projectile>().setBulletDamage(gunBullet.m_bulletDmg);
        bullet.GetComponent<Projectile>().setBulletRange(gunBullet.m_bulletRange);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * gunBullet.m_bulletSpeed;

        gunAmmo.m_curClipAmmo--;
    }

    protected virtual void AttackHandler()
    {
        if (gunAmmo.m_curClipAmmo > 0)
        {
            if (!gunBullet.m_Automatic)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (gunAmmo.m_curClipAmmo > 0)
                    {
                        Debug.Log("Semi-Auto fire called");
                        if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Recharge") &&
                            !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shot") &&
                            !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run"))

                            FireBullet();
                    }
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (gunAmmo.m_curClipAmmo > 0)
                    {
                        Debug.Log("Semi-Auto fire called");
                        if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Recharge") &&
                            !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shot") &&
                            !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run"))

                            FireBullet();
                    }
                }
                else if (Input.GetMouseButton(0))
                {
                    if (gunAmmo.m_curClipAmmo > 0)
                    {
                        Debug.Log("Automatic Fire Called");
                        if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Recharge") &&
                            !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shot") &&
                            !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run"))
                        {
                            m_Timer += Time.deltaTime;
                            if (m_Timer > gunBullet.m_rateOfFire)
                            {
                                FireBullet();
                                m_Timer = 0;
                            }
                        }
                    }
                }
            }
        }
     
    }

    protected virtual void ReloadHandler()
    {

        if (Input.GetKeyDown("r") && gunAmmo.m_curClipAmmo < gunAmmo.m_clipSize)
        {
            if (Player.GetComponent<FPS_Inventory>().GetWeaponAmmo(gunAmmo.m_WeaponID) > 0)
            {
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
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("isReloading"))
            {
                GetComponent<Animator>().SetBool("isReloading", false);
            }
        }
    }
    #endregion Projectile methods

    public int GetCurrentAmmo() { return gunAmmo.m_curClipAmmo; }
    public int GetMaxAmmo() { return gunAmmo.m_clipSize; }
    public int GetAmmoPool() { return gunAmmo.m_ammoPool; }

}
