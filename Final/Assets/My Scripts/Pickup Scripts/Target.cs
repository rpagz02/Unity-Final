using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TargetParamaters
{

    [Tooltip("Horizontal or Vertical Pace")]
    public bool Horizontal = true;
    [Tooltip("Pace Range")]
    public float Range = 3;
    [Tooltip("The Speed of motion")]
    public float speedMultiplier = 2;
    [Tooltip("Is the target an Enemy")]
    public bool Enemy = false;
    [Tooltip("Does the target move?")]
    public bool Static = true;
    [Tooltip("Does the target fire projectiles?")]
    public bool Agressive = false;
}
[System.Serializable]
public class TargetProjectileParams
{
    [Tooltip("Location on the player where the target looks")]
    public GameObject playerTargetLocation;
    [Tooltip("Location of projectile instantiation")]
    public GameObject shotLocation;
    [Tooltip("The Projectile Prefab")]
    public GameObject targetProjectile;
}
[System.Serializable]
public class TargetVisualFeedback
{
    [Tooltip("The shooting VFX")]
    public GameObject shotVFX;
    [Tooltip("The Text to display when shot")]
    public GameObject dmgText;
    [Tooltip("2 target lights that show feeback")]
    public GameObject targetLight1, targetLight2;
    [Tooltip("The 2 lights materials used")]
    public Material[] lightMats;
}



public class Target : MonoBehaviour
{

    #region Private Variables
    private string dmgString;
    private float timer;
    private float shotTimer;
    private bool damaged;
    [SerializeField]
    private bool Dead = false;
    private float minHorizontal, maxHorizontal, minVertical, maxVertical;
    public TargetParamaters paramaters;
    public TargetProjectileParams projectileParams;
    public TargetVisualFeedback feedback;
    #endregion Private Variables
    public bool GetDeathStatus()
    {
        return Dead;
    }
   


    
  
    #region Start and Update and CollisionEnter
    private void Start()
    {
        feedback.targetLight1.GetComponent<Renderer>().material = feedback.lightMats[0];
        feedback.targetLight2.GetComponent<Renderer>().material = feedback.lightMats[0];

        minHorizontal = transform.position.z;
        maxHorizontal = transform.position.z+ paramaters.Range;
        minVertical = transform.position.x;
        maxVertical = transform.position.x + paramaters.Range;
        timer = 0;
    }
    private void Awake()
    {
        projectileParams.playerTargetLocation = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {

        if (paramaters.Agressive)
        {
            shotTimer += Time.deltaTime;
            if (shotTimer >= 1)
            {
                Debug.Log("Should Fire a projectile");
                FireProjectile();
                shotTimer = 0;
            }
        }

        if (!paramaters.Static)
        {

            if (!paramaters.Horizontal)
                TargetPaceVertical();
            else
                TargetPaceHorizontal();

            if (!paramaters.Enemy)
                BasicTarget();
            else
                EnemyTarget();
        }
        else
        {
            if (!paramaters.Enemy)
                BasicTarget();
            else
                EnemyTarget();
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 8) // projectile layer
        {
            int dmg = (int)collision.gameObject.GetComponent<Projectile>().getBulletDamage();
            Destroy(collision.gameObject);
            dmgString = collision.gameObject.GetComponent<Projectile>().getBulletDamage().ToString("F1");
            damaged = true;
            
        }
    }
    #endregion Start and Update


    #region Target Movement Methods
    private void TargetPaceVertical()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time * paramaters.speedMultiplier, maxHorizontal - minHorizontal) + minHorizontal);

    }
    private void TargetPaceHorizontal()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * paramaters.speedMultiplier, maxVertical - minVertical) + minVertical, transform.position.y, transform.position.z);
    }
    public void SetSpeedMultiplier(float speed)
    {
        paramaters.speedMultiplier = speed;
    }
    #endregion Target Movement Methods



    private void BasicTarget()
    {
        if (damaged)
        {
            feedback.dmgText.GetComponent<Text>().text = dmgString;

            feedback.targetLight1.GetComponent<Renderer>().material = feedback.lightMats[1];
            feedback.targetLight2.GetComponent<Renderer>().material = feedback.lightMats[1];

            timer += Time.deltaTime;

            if (timer >= 0.25f)
            {
                feedback.targetLight1.GetComponent<Renderer>().material = feedback.lightMats[0];
                feedback.targetLight2.GetComponent<Renderer>().material = feedback.lightMats[0];

                feedback.dmgText.GetComponent<Text>().text = "";
                timer = 0;
                damaged = false;
            }
        }
    }

    private void EnemyTarget()
    {
        TargetObject();

        if (damaged)
        {
            paramaters.Static = true;
            paramaters.Agressive = false;


            feedback.dmgText.GetComponent<Text>().text = "Dead";
            Dead = true;
            GetComponent<DeathStatus>().SetDeathStatus(true);
            feedback.targetLight1.GetComponent<Renderer>().material = feedback.lightMats[1];
            feedback.targetLight2.GetComponent<Renderer>().material = feedback.lightMats[1];

            timer += Time.deltaTime;
        }
    }

    void FireProjectile()
    {
        Debug.Log("Fired a Projectile");
        feedback.shotVFX.GetComponent<ParticleSystem>().Play();
        Vector3 direction = projectileParams.shotLocation.transform.position;
        GameObject bullet = Instantiate(projectileParams.targetProjectile, direction, projectileParams.targetProjectile.transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 15;
    }

    private void TargetObject()
    {
        if (projectileParams.playerTargetLocation == null)
            return;

        Transform targetPosition = this.transform;
        if (projectileParams.playerTargetLocation != null)
            targetPosition = projectileParams.playerTargetLocation.transform;
        if (targetPosition)
        {
            Vector3 deltaPos = targetPosition.position - transform.position;
            Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(deltaPos), 1 * Time.deltaTime);
            transform.rotation = rot;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }
}
