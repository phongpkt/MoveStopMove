using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public Rigidbody rb;

    public float horizontal;
    public float vertical;
    public float speed = 10;

    private string currentAnim;
    public virtual void Moving()
    {

    }
    public virtual void Attack()
    {

    }
    public void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
}
