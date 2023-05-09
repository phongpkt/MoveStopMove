using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject weaponHolder;
    public Weapons weapon;
    public Rigidbody rb;

    //Set target
    public List<Character> Targets = new List<Character>();
    public Character target;
    private Vector3 TargetDirection;

    //Attack range
    public SphereCollider attackRangeCollider;
    public float AttackRange => BaseAttackRange * size;

    //Character data
    public float size;
    public float speed;
    public float BaseAttackRange;

    //Character action bool
    public bool isMoving;
    public bool isAttack;
    public bool isHit;

    //Animation
    public Animator animator;
    private string currentAnim;
    
    //StateMachine
    protected IState currentState;
    public IdleState IdleState = new();
    public PatrolState PatrolState = new();
    public AttackState AttackState = new();

    public virtual void OnEnable()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        size = 1f;
        speed = 10f;
        BaseAttackRange = 5f;
        ChangeState(IdleState);
    }
    public virtual void Update()
    {
        currentState.OnExecute(this);
    }
    //===========Moving==============
    public virtual void Moving() { }
    //===========Increase Size + Attack range==============
    public virtual void Grow()
    {
        size += 1;
        attackRangeCollider.radius = AttackRange;
    }

    //===========Attack==============
    public virtual void Attack()
    {
        TargetDirection = target.transform.position - transform.position;
        transform.LookAt(target.transform.position);
        ChangeState(AttackState);
        weaponHolder.SetActive(false);
        weapon.Shoot(TargetDirection);
    }
    public virtual void StopAttack()
    {
        weaponHolder.SetActive(true);
        ChangeState(IdleState);
    }

    //===========Die==============
    public virtual void Die()
    {
        ChangeAnim(Constants.ANIM_DEAD);
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
    //===========States==============
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
