using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] Weapons weapon;
    public GameObject player;
    public Animator animator;
    public Rigidbody rb;

    public float horizontal;
    public float vertical;
    public float speed = 10;
    public float attackRange;

    public bool isMoving;
    public bool isAttack;
    public bool isHit;
    private string currentAnim;
    public virtual void Moving()
    {

    }
    public virtual void Attack()
    {

    }
    public virtual void Die()
    {

    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.SetBool(currentAnim, false);
            currentAnim = animName;
            animator.SetBool(currentAnim, true);
        }
    }
}
