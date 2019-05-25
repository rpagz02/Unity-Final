using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BasicTurretParamaters
{
    [Header("ACTIVE STATUS")]
    [Tooltip("Activate or deactivate the Turret")]
    public bool active = true;
    public bool canFire = false;

    [Header("TARGETING")]
    [Tooltip("Position of where the raycast should be shot from to detect range")]
    public GameObject RangeDetectionEye;
    [Tooltip("Object to Target")]
    public GameObject Target;
    [Tooltip("Speed of aiming at the target")]
    public float aimingSpeed = 1;
    [Tooltip("Rate of Fire Multiplier")]
    [Range(0.5f, 2f)]
    public float ROF = 0.5f;
    [Tooltip("Range of Detection")]
    [Range(0, 100)]
    public float Range = 18.5f;
    [Tooltip("This light signifies whether the player is in range or not")]
    public GameObject TargetLight;
    [Header("Target Light Materials")]
    public Material[] LightMats;

    [Header("HEALTH")]
    [Tooltip("Heat Meter Bar")]
    public GameObject Temperature;
    [Header("Colors to Lerp Between")]
    public Material[] pulseColors;
    [Header("Pulse Duration")]
    public float duration;
}

[System.Serializable]
public class BasicTurretHealth
{
    [Header("Health Variables")]
    [Tooltip("The Turrets Maximum Health")]
    public float maxHealth = 100;
    [Tooltip("The Turrets Current Health")]
    public float currentHealth = 0;
    [Space(10)]
    [Header("UI Variables")]
    public GameObject[] healthBlocks;
    [Header("Object to Destroy at no health")]
    public GameObject TheTurret;
    [Space(10)]
    [Header("Explosion VFX")]
    [Tooltip("This is the explosion fx played at no health")]
    public GameObject VFX;
    public GameObject Orb;


}


public class BasicTurret : MonoBehaviour
{
    //temp (delete this later)
    [SerializeField]
    private float distance;

    public BasicTurretParamaters turretParams;
    public BasicTurretHealth turretHealth;
    private float timer = 0;
    private float shotTimer = 0;
    private GameObject Player;

    private void Awake()
    {
        turretHealth.currentHealth = turretHealth.maxHealth;
        Player = GameObject.FindGameObjectWithTag("Player");
        turretHealth.Orb.GetComponent<Animator>().speed = turretParams.ROF;
    }
    private void Update()
    {
        HealthMonitor();

        distance = Vector3.Distance(transform.position, Player.transform.position);
    }


    void FixedUpdate()
    {
        
        ColorPulse();

        RangeDetection();

        if (turretParams.active == true && turretParams.canFire == true)
        {
            turretHealth.Orb.GetComponent<Animator>().speed = turretParams.ROF;
            TargetObject();
        }
        else
            turretHealth.Orb.GetComponent<Animator>().speed = 0;
    }

    private void TargetObject()
    {
        Debug.Log("Shuold be targeting");
        if (turretParams.Target == null)
            return;

        Transform targetPosition = null;
        if (turretParams.Target != null)
            targetPosition = turretParams.Target.transform;
        if (targetPosition)
        {
            Vector3 deltaPos = targetPosition.position - transform.position;
            Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(deltaPos), turretParams.aimingSpeed * Time.deltaTime);
            transform.rotation = rot;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }

    private void RangeDetection()
    {
        RaycastHit hit;
        Debug.DrawRay(turretParams.RangeDetectionEye.transform.position, (Player.transform.position - turretParams.RangeDetectionEye.transform.position), Color.red);

        if (Vector3.Distance(turretParams.RangeDetectionEye.transform.position, Player.transform.position) < turretParams.Range)
        {
            if (Physics.Raycast(turretParams.RangeDetectionEye.transform.position, (Player.transform.position - turretParams.RangeDetectionEye.transform.position), out hit, turretParams.Range))
            {
                if (hit.transform.tag == "Player")
                {
                    Debug.Log("Player is in Range of Turret!");
                    Debug.DrawRay(turretParams.RangeDetectionEye.transform.position, (Player.transform.position - turretParams.RangeDetectionEye.transform.position), Color.green);
                    turretParams.TargetLight.GetComponent<Renderer>().material = turretParams.LightMats[1];
                    turretParams.canFire = true;
                    turretHealth.Orb.GetComponent<GlowingOrb>().setFireStatus(true);
                }
                else
                {
                    turretParams.TargetLight.GetComponent<Renderer>().material = turretParams.LightMats[0];
                    turretParams.canFire = false;
                    turretHealth.Orb.GetComponent<GlowingOrb>().setFireStatus(false);
                }
            }
        }
        else
        {
            turretParams.TargetLight.GetComponent<Renderer>().material = turretParams.LightMats[0];
            turretParams.canFire = false;
            turretHealth.Orb.GetComponent<GlowingOrb>().setFireStatus(false);
        }
    }
    private void ColorPulse()
    {
        float lerp = Mathf.PingPong(Time.time, turretParams.duration) / turretParams.duration;
        turretParams.Temperature.GetComponent<Renderer>().material.Lerp(turretParams.pulseColors[0], turretParams.pulseColors[1], lerp);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            turretHealth.currentHealth -= collision.gameObject.GetComponent<Projectile>().getBulletDamage();
        }
    }

    private void HealthMonitor()
    {
        // if 0% blocks of health
        if (turretHealth.currentHealth <= 0)
        {
            turretHealth.healthBlocks[0].SetActive(false);
            turretHealth.healthBlocks[1].SetActive(false);
            turretHealth.healthBlocks[2].SetActive(false);
            turretHealth.healthBlocks[3].SetActive(false);
            turretHealth.healthBlocks[4].SetActive(false);
            if (!turretHealth.VFX.GetComponent<ParticleSystem>().isPlaying)
            {
                Instantiate(turretHealth.VFX, this.transform.position, this.transform.rotation);
                Destroy(turretHealth.TheTurret);
            }
        }
        // 20% block of health
        if (turretHealth.currentHealth > 0 && turretHealth.currentHealth < (turretHealth.maxHealth * 0.4f))
        {
            turretHealth.healthBlocks[0].SetActive(true);
            turretHealth.healthBlocks[1].SetActive(false);
            turretHealth.healthBlocks[2].SetActive(false);
            turretHealth.healthBlocks[3].SetActive(false);
            turretHealth.healthBlocks[4].SetActive(false);
        }
         // 40% block of health
        if (turretHealth.currentHealth >= (turretHealth.maxHealth * 0.4f) && turretHealth.currentHealth < (turretHealth.maxHealth * 0.6f))
        {
            turretHealth.healthBlocks[0].SetActive(true);
            turretHealth.healthBlocks[1].SetActive(true);
            turretHealth.healthBlocks[2].SetActive(false);
            turretHealth.healthBlocks[3].SetActive(false);
            turretHealth.healthBlocks[4].SetActive(false);
        }
        // 60% block of health
        if (turretHealth.currentHealth >= (turretHealth.maxHealth * 0.6f) && turretHealth.currentHealth < (turretHealth.maxHealth * 0.8f))
        {
            turretHealth.healthBlocks[0].SetActive(true);
            turretHealth.healthBlocks[1].SetActive(true);
            turretHealth.healthBlocks[2].SetActive(true);
            turretHealth.healthBlocks[3].SetActive(false);
            turretHealth.healthBlocks[4].SetActive(false);
        }
        // 80% block of health
        if (turretHealth.currentHealth >= (turretHealth.maxHealth * 0.8f) && turretHealth.currentHealth < (turretHealth.maxHealth * 0.9f))
        {
            turretHealth.healthBlocks[0].SetActive(true);
            turretHealth.healthBlocks[1].SetActive(true);
            turretHealth.healthBlocks[2].SetActive(true);
            turretHealth.healthBlocks[3].SetActive(true);
            turretHealth.healthBlocks[4].SetActive(false);
        }
        // 90+ % block of health
        if (turretHealth.currentHealth >= (turretHealth.maxHealth * 0.9f) && turretHealth.currentHealth <= turretHealth.maxHealth)
        {
            turretHealth.healthBlocks[0].SetActive(true);
            turretHealth.healthBlocks[1].SetActive(true);
            turretHealth.healthBlocks[2].SetActive(true);
            turretHealth.healthBlocks[3].SetActive(true);
            turretHealth.healthBlocks[4].SetActive(true);
        }

    }

}
