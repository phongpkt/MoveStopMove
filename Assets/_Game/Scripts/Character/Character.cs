using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : GameUnit
{
    public GameObject weaponHolder;
    public Weapons weapon;
    public Rigidbody rb;

    //Set target
    public List<Character> Targets = new List<Character>();
    public Character currentTarget;
    private Vector3 targetDirection;

    //Attack range
    public SphereCollider attackRangeCollider;
    private float attackRange => baseAttackRange * size;

    //Character data
    public float size;
    public float speed;
    public float attackIntervalTimer;
    public float attackInterval;
    [HideInInspector] public float baseAttackRange;

    //Character action bool
    public bool isHit;
    public bool isAttack;

    //Animation
    public Animator animator;
    private string currentAnim;

    //StateMachine
    protected IState currentState;
    public IdleState IdleState = new();
    public RunState RunState = new();
    public PatrolState PatrolState = new();
    public AttackState AttackState = new();
    public DeadState DeadState = new();


    public virtual void OnEnable()
    {
        OnInit();
    }
    public override void OnInit()
    {
        size = 1f;
        speed = 10f;
        baseAttackRange = 1f;
        attackInterval = 2f;
        attackIntervalTimer = attackInterval;
        //attackRangeCollider.radius = attackRange;
        ChangeState(IdleState);
    }
    public override void OnDespawn() { }
    public virtual void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    //===========Moving==============
    public virtual void Moving() { }
    public virtual void Patrol() { }
    public virtual void ResetPatrol() { }
    public virtual void StopPatrol() { }
    public virtual void FindDirection() { }

    //===========Increase Size + Attack range==============
    public virtual void Grow()
    {
        size += 1;
        attackRangeCollider.radius = attackRange;
    }

    //===========Attack==============
    public virtual void CheckAroundCharacters() 
    {
        if (Targets.Count != 0)
        {
            currentTarget = Targets[0];
            isAttack = true;
            ChangeState(AttackState);
        }
    }
    public virtual void LookAtTarget()
    {
        if (currentTarget != null)
        {
            Vector3 lookDirection = currentTarget.transform.position - transform.position;
            lookDirection.y = 0f;
            lookDirection = lookDirection.normalized;
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }
    public virtual void Attack()
    {
        targetDirection = currentTarget.transform.position - transform.position;
        weaponHolder.SetActive(false);
        weapon.Shoot(gameObject.GetComponent<Character>(),targetDirection);
    }
    public virtual void ResetAttack()
    {
        if (isAttack)
        {
            attackIntervalTimer -= Time.deltaTime;
            if (attackIntervalTimer <= 0)
            {
                ChangeState(IdleState);
                attackIntervalTimer = attackInterval;
            }
        }
    }
    public virtual void StopAttack()
    {
        weaponHolder.SetActive(true);
        isAttack = false;
    }

    //===========Die==============
    public virtual void Die()
    {
        ChangeState(DeadState);
    }

    //===========Animation==============
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
    //===========StateController==============
    public virtual void ChangeState(IState state)
    {
        if (currentState == state)
            return;
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

}
