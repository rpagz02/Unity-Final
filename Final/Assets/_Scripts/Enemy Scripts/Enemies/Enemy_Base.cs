using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Enemy_Health
{
    [Header("Enemy Health Variables")]
    [Tooltip("Is the enemy alive or dead")]
    public bool m_Alive;
    [Tooltip("This enemy's maximum health")]
    public float m_maxHealth;
    [Tooltip("This enemy's current health")]
    public float m_currentHealth;
}

[System.Serializable]
public class Enemy_Damage
{ 
    [Header("Enemy Damage Output Variables")]
    [Tooltip("This enemy's base damage amnt")]
    public float m_baseDamage;
    public GameObject[] AttackColliders;
}

[System.Serializable]
public class Enemy_Targeting
{
    [Header("Enemy Targeting Variables listed below")]
    [Tooltip("Drag and drop the eyes here (for raycasting)")]
    public GameObject Eyes;
    [Tooltip("The Range at which the player must be to trigger pursuit")]
    public float m_pursuitRange;
    [Tooltip("The Range at which the player must be to trigger pursuit")]
    public float m_chaseRange;
    [Tooltip("The Range at which the Enemy will stop pursuing the player")]
    public float m_attackRange;
    [Tooltip("The Speed at which the Enemy will pursue the player")]
    public float m_chaseSpeed;
    [Tooltip("The radius the enemy can wander within")]
    public float m_wanderRadius;
    [Tooltip("The interval duration between wandering")]
    public float m_wanderInterval;

    [Space(2)]

    [Header("Drag and Drop Variables")]
    [Tooltip("GameObject to Target")]
    public GameObject Player;
    [Tooltip("Array of Patrol Points")]
    public GameObject[] PatrolPoints;
}

[System.Serializable]
public class Enemy_FX
{
    [Header("VFX Variables")]
    public GameObject BloodDecal;
    public GameObject BloodVFX;
    public GameObject SpawnVFX;
    public GameObject FireVFX;

    [Header("SFX Variables")]
    public GameObject ShoutSFX;
    public GameObject FootStepSFX; 
}


public abstract class Enemy_Base : MonoBehaviour
{
    #region Variables
    [Header("States the Enemy can be in")]
    public StateMachine m_StateMachine;
    [Space(2)]
    [Header("Enemy Health Variables")]
    public Enemy_Health EnemyHealth;
    [Header("Enemy Damage Variables")]
    public Enemy_Damage EnemyDamage;
    [Header("Enemy Targeting Variables")]
    public Enemy_Targeting EnemyTargeting;
    [Header("Enemy FX Variables")]
    public Enemy_FX EnemyFX;
    #endregion Variables

    #region Start and Update

    public virtual void Update()
    {

    } 
    
        public void TakeDamage(float amnt)
    {
        if (EnemyHealth.m_Alive)
        {
                EnemyHealth.m_currentHealth -= amnt;
                if (EnemyHealth.m_currentHealth < 0)
                {
                    EnemyHealth.m_currentHealth = 0;
                    EnemyHealth.m_Alive = false;
                }          
        }
    }
    #endregion Start and Update

}
