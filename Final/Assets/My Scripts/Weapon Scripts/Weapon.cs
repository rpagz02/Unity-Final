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
    protected private float m_Timer;
    public GameObject m_shotPoint;
    public GameObject m_projectile;
    public GameObject VFX;
    protected private AudioSource SFX;
    public AudioClip reloadSFX;
    public AudioClip shotSFX;
    public GameObject reticle_Image;
    #endregion Base Weapon Variables


    private void Start()
    {

        // toggled used for ADS method
        toggle = false;
        Player = GameObject.FindGameObjectWithTag("Player");
        SFX = GetComponent<AudioSource>();
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
        reticle_Image.SetActive(!toggle);
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
        VFX.GetComponent<ParticleSystem>().Play();
        SFX.pitch = Random.Range(0.8f, 1f);
        SFX.PlayOneShot(shotSFX, 0.5f);

        Vector3 direction = m_shotPoint.transform.position + Random.insideUnitSphere * 0.1f;
        GameObject bullet = Instantiate(m_projectile, direction, m_shotPoint.transform.rotation);
        bullet.GetComponent<Projectile>().setBulletDamage(m_bulletDmg);
        bullet.GetComponent<Projectile>().setBulletRange(m_bulletRange);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * m_bulletSpeed;

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
                    if (m_curClipAmmo > 0)
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
                    if (m_curClipAmmo > 0)
                    {
                        Debug.Log("Automatic Fire Called");
                        if (!GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Recharge") &&
                            !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Shot") &&
                            !GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Run"))
                        {
                            m_Timer += Time.deltaTime;
                            if (m_Timer > m_rateOfFire)
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

    public int GetCurrentAmmo() { return m_curClipAmmo; }
    public int GetMaxAmmo() { return m_clipSize; }
    public int GetAmmoPool() { return m_ammoPool; }

}
