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
    protected IState currentState;
    private string currentAnim;

    public virtual void OnEnable()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        size = 1f;
        speed = 10f;
        BaseAttackRange = 5f;
        ChangeAnim(Constants.ANIM_IDLE);
    }
    public virtual void Update()
    {

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
        Debug.Log(3);
        if (isAttack)
        {
            TargetDirection = target.transform.position - transform.position;
            transform.LookAt(target.transform.position);
            //ChangeAnim(Constants.ANIM_ATTACK);
            ChangeState(new AttackState());
            weaponHolder.SetActive(false);
            weapon.Shoot(TargetDirection);
        }
    }
    public virtual void StopAttack()
    {
        isAttack = false; 
        weaponHolder.SetActive(true);
    }

    //===========Die==============
    public virtual void Die()
    {
        if(isHit)
        {
            ChangeAnim(Constants.ANIM_DEAD);
        }
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
