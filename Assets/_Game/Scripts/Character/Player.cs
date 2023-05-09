using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FloatingJoystick joystick;
    private float horizontal;
    private float vertical;
    private Vector3 direction;

    private void FixedUpdate()
    {
        Moving();
    }
    public override void Attack()
    {
        base.Attack();
    }
    public override void Moving()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
        if(horizontal != 0 || vertical != 0)
        {
            isMoving = true;
            direction = (Vector3.forward * vertical + Vector3.right * horizontal) * speed;
            rb.velocity = direction;

            Vector3 lookDirection = direction;
            if (Mathf.Abs(lookDirection.magnitude) > 0.01f)
            {

                Quaternion rotate = Quaternion.LookRotation(lookDirection, Vector3.up);
                transform.rotation = rotate;
            }
            ChangeAnim(Constants.ANIM_RUN);
        }
        else if(!isAttack)
        {
            isMoving = false;
            rb.velocity = Vector3.zero;
            ChangeAnim(Constants.ANIM_IDLE);
        }
    }
}
