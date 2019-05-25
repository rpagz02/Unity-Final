using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hellion : Enemy_Base
{
    // Local Variables
    private Rigidbody[] rbs;
    private NavMeshAgent agent;
    private GameObject Player;
    private Animator animator;
    Animator animController;

    private bool Fireball = false;
    private bool PlayerTargeted = false;
    private bool Attacking = false;
    private bool StateKilled = false;
    private float GenTimer = 0;
    private float AttackInterval;

    public GameObject shotPos;
    public GameObject Projectile;


    void Start()
    {
        animator = GetComponent<Animator>();
        AttackInterval = Random.Range(3.2f, 5.3f); ;
        rbs = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in rbs){rb.isKinematic = true;}
        Player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        m_StateMachine = new StateMachine();
        //////////////////////////////////////////////////////////

        EnemyDamage.m_baseDamage = 20;

        EnemyTargeting.m_attackRange = 3.5f;
        EnemyTargeting.m_pursuitRange = 25;
        EnemyTargeting.m_chaseSpeed = Random.Range(3, 7f);
        agent.speed = EnemyTargeting.m_chaseSpeed;
        EnemyTargeting.Player = Player;


        EnemyHealth.m_maxHealth = 125f;
        EnemyHealth.m_currentHealth = 125f;
        EnemyHealth.m_Alive = true;

        for (int i = 0; i < EnemyDamage.AttackColliders.Length; i++)
        {
            EnemyDamage.AttackColliders[i].GetComponent<MelleCollider>().SetMeleeDamage(EnemyDamage.m_baseDamage);
        }

        // Set Hellions Initial state to WANDER
        this.m_StateMachine.ChangeState(new State_Idle(this.gameObject));
    }

    public override void Update()
    {
        this.m_StateMachine.RunCurrentState();
        HealthLogic();
        if (!StateKilled)
        {
            PrePursuitLogic();
            PursuitLogic();
            MelleColliderLogic();
        }
        else
            Destroy(this, 2f);

    }

    private void PrePursuitLogic()
    {
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            // If the player is within my pursuit range
            if (ETargetingUtils.AI_TargetByDistance(this.gameObject, Player, EnemyTargeting.m_pursuitRange))
            {
                // If I can see the player...
                if (ETargetingUtils.AI_TargetBySight(EnemyTargeting.Eyes, this.gameObject, Player, EnemyTargeting.m_pursuitRange))
                {
                    this.m_StateMachine.ChangeState(new State_Chase(this.gameObject));
                    PlayerTargeted = true;
                }
            }
        }
    }
    private void PursuitLogic()
    {
        if (PlayerTargeted)
        {
            if (!Attacking)
            {
                GenTimer += Time.deltaTime;
                if (GenTimer >= AttackInterval)
                {
                    this.m_StateMachine.ChangeState(new State_RangedAttack(this.gameObject));
                    Fireball = true;
                    GenTimer = 0;
                }
                this.m_StateMachine.ChangeState(new State_Chase(this.gameObject));
                if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Fireball Attack"))
                    agent.isStopped = true;
            }
            if (ETargetingUtils.AI_TargetByDistance(this.gameObject, Player, EnemyTargeting.m_attackRange))
            {
                Attacking = true;
                agent.isStopped = true;
                animator.SetBool("Attack", true);
            }
            else
            {
                animator.SetBool("Attack", false);
                Attacking = false;
            }

        }
    }
    private void HealthLogic()
    {
        if (EnemyHealth.m_Alive == false && StateKilled == false)
        {
            this.m_StateMachine.ChangeState(new State_Ragdoll(this.gameObject));
            StateKilled = true;
        }
    }
    private void MelleColliderLogic()
    {
        if (Attacking)
        {
            for (int i = 0; i < EnemyDamage.AttackColliders.Length; i++)
            {
                EnemyDamage.AttackColliders[i].GetComponent<Collider>().enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < EnemyDamage.AttackColliders.Length; i++)
            {
                EnemyDamage.AttackColliders[i].GetComponent<Collider>().enabled = false;
            }
        }
    }

    // Helper Methods
    public void FireProjectile()
    {
        Debug.Log("Fireball Thrown");
        Vector3 direction = Player.transform.position - shotPos.transform.position;
        GameObject bullet = Instantiate(Projectile, shotPos.transform.position, Quaternion.LookRotation((direction)));
        bullet.GetComponent<EnemyProjectile>().SetProjectileDamage(EnemyDamage.m_baseDamage + 15);
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20.2f;
    }
}
