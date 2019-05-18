using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walker : Enemy_Base
{
    private float timer;
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
        EnemyTargeting.m_chaseRange = 15;
        EnemyTargeting.m_chaseSpeed = 3;
        EnemyTargeting.m_wanderRadius = 6;
        EnemyTargeting.m_Chase = false;
        EnemyTargeting.Player = GameObject.FindGameObjectWithTag("Player");

        EnemyHealth.m_maxHealth = 55f;
        EnemyHealth.m_currentHealth = 55f;
        EnemyHealth.m_Alive = true;

        m_State = State.WANDER;
        #endregion Setting Variables
    }

    public override void Update()
    {
        if (agent.velocity == Vector3.zero) animController.SetBool("Walk", false);
        else animController.SetBool("Walk", true);
        TargetingLogic();
        StateMachineLogic();
    }

    private void StateMachineLogic()
    {
        switch (m_State)
        {
            case State.SPAWN:
                {
                    break;
                }
            case State.IDLE:
                {
                    agent.isStopped = true;
                    animController.SetBool("Walk", false);
                    break;
                }
            case State.WANDER:
                {
                    timer += Time.deltaTime;
                    if (timer >= EnemyTargeting.m_wanderRadius)
                    {
                        ETargetingUtils.AI_Wander(this.gameObject);
                        timer = 0;
                    }
                    break;
                }
            case State.CHASE:
                {
                    if(Player != null)
                    ETargetingUtils.AI_Chase(this.gameObject, Player);

                    if (Vector3.Distance(transform.position, Player.transform.position) >= EnemyTargeting.m_chaseRange)
                    {
                        m_State = State.WANDER;
                        EnemyTargeting.m_Chase = false;
                    }
                    else if (Vector3.Distance(transform.position, Player.transform.position) <= EnemyTargeting.m_attackRange)
                    {
                        m_State = State.ATTACK;
                        EnemyTargeting.m_Chase = false;
                    }

                    break;
                }
            case State.ATTACK:
                {

                    break;
                }
            case State.HURT:
                {

                    break;
                }
            case State.DEATH:
                {

                    break;
                }
            default:
                {

                    break;
                }
        }
    }

    // 
    private void TargetingLogic()
    {
        if (ETargetingUtils.AI_Target(EnemyTargeting.Eyes, this.gameObject, EnemyTargeting.m_pursuitRange))
            EnemyTargeting.m_Chase = true;

        if (EnemyTargeting.m_Chase == true)
            m_State = State.CHASE;
    }
}
