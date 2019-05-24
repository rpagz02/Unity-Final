using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharredWalker : Enemy_Base
{
    private Rigidbody[] rbs; 
    private NavMeshAgent agent;
    private GameObject Player;
    Animator animController;

    private bool TargetingPlayer = false;
    private bool Wandering = false;
    private bool Attacking = false;
    private bool StateKilled = false;


    void Start()
    {
        #region Variables
        rbs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = true;
        }

        agent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        m_StateMachine = new StateMachine();

        EnemyDamage.m_baseDamage = 15;
        Player = GameObject.FindGameObjectWithTag("Player");
        EnemyTargeting.m_attackRange = 2.5f;
        EnemyTargeting.m_pursuitRange = 12;
        EnemyTargeting.m_chaseRange = 18f;
        EnemyTargeting.m_chaseSpeed = Random.Range(2f, 3.5f);
        EnemyTargeting.m_wanderRadius = Random.Range(3.5f, 8f);
        EnemyTargeting.m_wanderInterval = Random.Range(2.1f, 4.3f);
        EnemyTargeting.Player = Player;

        EnemyHealth.m_maxHealth = 115f;
        EnemyHealth.m_currentHealth = 115f;
        EnemyHealth.m_Alive = true;

        // Set Walkers Initial state to WANDER
        this.m_StateMachine.ChangeState(new State_Wander(this.gameObject, EnemyTargeting.m_wanderInterval, EnemyTargeting.m_wanderRadius, EnemyTargeting.m_chaseSpeed));

        for (int i = 0; i < EnemyDamage.AttackColliders.Length; i++)
        {
            EnemyDamage.AttackColliders[i].GetComponent<MelleCollider>().SetMeleeDamage(EnemyDamage.m_baseDamage);
        }
        #endregion Variables
    }

    public override void Update()
    {

        // First we run the WANDER State
        this.m_StateMachine.RunCurrentState();
        // Then we proceed with our state machine logic

        if (!StateKilled)
        {
            if (ETargetingUtils.AI_Target(EnemyTargeting.Eyes, this.gameObject, EnemyTargeting.m_pursuitRange) && !TargetingPlayer)
            {
                this.m_StateMachine.ChangeState(new State_Chase(this.gameObject));
                TargetingPlayer = true;
                Attacking = false;
            }
            TargetingLogic();
            MelleColliderLogic();
        }




        if(EnemyHealth.m_Alive == false && StateKilled == false)
        {

            this.m_StateMachine.ChangeState(new State_Ragdoll(this.gameObject));
            StateKilled = true;
        }
        if (StateKilled) Destroy(this.gameObject, 5f);

    }

    // Checks the Distance from the player and acts accordinly 
    private void TargetingLogic()
    {
        // Perfrom our 3 distance checks here
        // If We're in Attack Range - Attack
        if (Vector3.Distance(transform.position, Player.transform.position) <= EnemyTargeting.m_attackRange && !Attacking)
        {
            Attacking = true;
            this.m_StateMachine.ChangeState(new State_Attack(this.gameObject));
        }

        // If we're out of Attack Range but still in pursuit range -> Chase
        if (Vector3.Distance(transform.position, Player.transform.position) > EnemyTargeting.m_attackRange && Vector3.Distance(transform.position, Player.transform.position) < EnemyTargeting.m_pursuitRange)
        {
            Attacking = false;
            this.m_StateMachine.ChangeState(new State_Chase(this.gameObject));
        }
        // If we're out of Attack Range and out of Pursuit Range -> Wander
        else if (Vector3.Distance(transform.position, Player.transform.position) > EnemyTargeting.m_attackRange && Vector3.Distance(transform.position, Player.transform.position) > EnemyTargeting.m_pursuitRange)
        {
            Attacking = false;
            if (TargetingPlayer)
                this.m_StateMachine.ChangeState(new State_Wander(this.gameObject, EnemyTargeting.m_wanderInterval, EnemyTargeting.m_wanderRadius, EnemyTargeting.m_chaseSpeed));
            TargetingPlayer = false;
        }
    }
    
    // If we're attacking, set the melee colliders active, or incative respectively 
    private void MelleColliderLogic()
    {
        if (Attacking)
        {
            for (int i = 0; i < EnemyDamage.AttackColliders.Length; i++)
            {
                EnemyDamage.AttackColliders[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < EnemyDamage.AttackColliders.Length; i++)
            {
                EnemyDamage.AttackColliders[i].SetActive(true);
            }
        }
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
}
