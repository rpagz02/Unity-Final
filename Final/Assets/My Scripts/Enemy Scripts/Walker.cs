using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walker : Enemy_Base
{
    private float timer;
    private int startledStrikes;
    private bool EnemyStartled = false;
    private GameObject Player;
    private NavMeshAgent agent;
    private Animator animController;

    void Start()
    {
        #region Setting Variables
        Player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        timer = 0;
        animController = GetComponent<Animator>();

        EnemyDamage.m_baseDamage = 15;

        EnemyTargeting.m_attackRange = 2;
        EnemyTargeting.m_pursuitRange = 7;
        EnemyTargeting.m_chaseRange = 9;
        EnemyTargeting.m_chaseSpeed = 2;
        EnemyTargeting.m_wanderRadius = 6;
        EnemyTargeting.m_wanderInterval = 4;
        EnemyTargeting.Player = GameObject.FindGameObjectWithTag("Player");

        EnemyHealth.m_maxHealth = 115f;
        EnemyHealth.m_currentHealth = 115f;
        EnemyHealth.m_Alive = true;


        EnemyTargeting.m_Chase = false;
        EnemyTargeting.m_Attacking = false;

        m_State = State.SPAWN;
        #endregion Setting Variables
    }

    public override void Update()
    {
        AnimationLogic();
        StateMachineLogic();
    }

    private void StateMachineLogic()
    {
        switch (m_State)
        {
            case State.SPAWN:
                {
                    m_State = State.WANDER;
                    break;
                }
            case State.WANDER:
                {
                    timer += Time.deltaTime;
                    if (timer >= EnemyTargeting.m_wanderInterval)
                    {
                        ETargetingUtils.AI_Wander(this.gameObject, EnemyTargeting.m_wanderRadius);
                        timer = 0;
                    }
                    if (ETargetingUtils.AI_Target(EnemyTargeting.Eyes, this.gameObject, EnemyTargeting.m_pursuitRange))
                        m_State = State.CHASE;

                    break;
                }
            case State.CHASE:
                {
                    LookAtPlayer();
                    if (Player != null)
                        ETargetingUtils.AI_Chase(this.gameObject, Player);

                    if (Vector3.Distance(transform.position, Player.transform.position) >= EnemyTargeting.m_chaseRange)
                    {
                        m_State = State.WANDER;
                    }
                    else if (Vector3.Distance(transform.position, Player.transform.position) <= EnemyTargeting.m_attackRange)
                    {
                        m_State = State.ATTACK;
                    }

                    break;
                }
            case State.ATTACK:
                {
                    LookAtPlayer();
                    EnemyTargeting.m_Attacking = true;
                    EAttackUtils.AI_MeleeAttack(this.gameObject, EnemyDamage.AttackColliders);

                    if (Vector3.Distance(transform.position, Player.transform.position) > EnemyTargeting.m_attackRange)
                    {
                        EnemyTargeting.m_Attacking = false;
                        m_State = State.CHASE;
                        break;
                    }                    
                    break;
                }
            case State.DEATH:
                {
                    agent.isStopped = true;
                    break;
                }
        }
    }

    private void AnimationLogic()
    {
        if (EnemyHealth.m_Alive)
        {
            if (agent.velocity == Vector3.zero)
                animController.SetBool("Walk", false);
            else if (agent.velocity != Vector3.zero)
                animController.SetBool("Walk", true);

            if (EnemyTargeting.m_Attacking)
            {
                animController.SetBool("Walk", false);
                animController.SetBool("Attack", true);
            }
            else
                animController.SetBool("Attack", false);
        }
        else
        {
            animController.SetBool("Attack", false);
            animController.SetBool("Walk", false);
            animController.SetTrigger("Die");
        }
    }
    private void LookAtPlayer()
    {
        Vector3 lookVector = Player.transform.position - transform.position;
        lookVector.y = transform.position.y;
        Quaternion rot = Quaternion.LookRotation(lookVector);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (EnemyHealth.m_Alive)
        {
            startledStrikes++;
            LookAtPlayer();
            if (collision.gameObject.layer == 8)
            {
                EnemyHealth.m_currentHealth -= collision.gameObject.GetComponent<Projectile>().getBulletDamage();
                if (EnemyHealth.m_currentHealth < 0)
                {
                    EnemyHealth.m_currentHealth = 0;
                    EnemyHealth.m_Alive = false;
                    m_State = State.DEATH;
                }
            }

            if (EnemyHealth.m_Alive && startledStrikes >= 3)
            {
                startledStrikes = 0;
                m_State = State.CHASE;
            }
        }
    }

}
