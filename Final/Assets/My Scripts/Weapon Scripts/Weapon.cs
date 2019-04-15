using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public enum Weapons { Knife, Pistol, SMG, Shotgun, Rifle, LMG, Crossbow, GrenadeLauncher, Axe, Grenade, Flashlight };

    #region Base Weapon Variables
    //Unique Weapon ID
    [SerializeField]
    protected int m_WeaponID;
    // Ammunition Variables
    [SerializeField]
    protected int m_clipSize;
    [SerializeField]
    protected int m_curClipAmmo;
    [SerializeField]
    protected int m_ammoPool;
    // Projectile Variables
    [SerializeField]
    protected float m_bulletDmg;
    [SerializeField]
    protected float m_bulletSpeed;
    [SerializeField]
    protected float m_shotRecoil;
    [SerializeField]
    protected float m_bulletRange;
    [SerializeField]
    protected float m_rateOfFire;
    [SerializeField]
    protected bool m_Automatic;

    protected private bool toggle;
    protected private GameObject Player;
    protected private bool isReloading;
    public GameObject m_shotPoint;
    public GameObject m_projectile;
   

    #endregion Base Weapon Variables

    public Weapon()
    {

        isReloading = false;
        m_clipSize = 0;
        m_curClipAmmo = 0;
        m_ammoPool = 0;
        m_bulletDmg = 0;
        m_bulletRange = 0;
        m_bulletSpeed = 0;
        m_shotRecoil = 0;
        m_rateOfFire = 0;
        m_Automatic = false;
        Debug.Log("Base Weapon Constructor Called");
    }

    private void Start()
    {
        // toggled used for ADS method
        toggle = false;
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        m_ammoPool = Player.GetComponent<FPS_Inventory>().GetWeaponAmmo(m_WeaponID);

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

        Vector3 direction = m_shotPoint.transform.position;
        GameObject bullet = Instantiate(m_projectile, direction, m_shotPoint.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * m_bulletSpeed;

        Destroy(bullet, 2.0f);

        m_curClipAmmo--;
    }

    protected virtual void AttackHandler()
    {
        if (m_curClipAmmo > 0)
        {
            if (!m_Automatic)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (m_curClipAmmo > 0)
                    {
                        Debug.Log("Semi-Auto fire called");
                        if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Recharge") &&
                            !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shot"))
                            FireBullet();
                    }
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Debug.Log("Automatic Fire Called");
                    FireBullet();
                }
            }
        }
        //else      
            //Debug.Log("Out of Ammo!");      
    }

    protected virtual void ReloadHandler()
    {

        if (Input.GetKeyDown("r") && m_curClipAmmo < m_clipSize)
        {
            if (Player.GetComponent<FPS_Inventory>().GetWeaponAmmo(m_WeaponID) > 0)
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
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("isReloading"))
            {
                GetComponent<Animator>().SetBool("isReloading", false);
            }
        }
    }
   
    #endregion Projectile methods
}
