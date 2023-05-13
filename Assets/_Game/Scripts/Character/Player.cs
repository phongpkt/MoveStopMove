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
    public override void Moving()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
        direction = (Vector3.forward * vertical + Vector3.right * horizontal) * speed;
        if(direction.magnitude != 0)
        {
            rb.velocity = direction;
            Vector3 lookDirection = direction;
            if (Mathf.Abs(lookDirection.magnitude) > 0.01f)
            {

                Quaternion rotate = Quaternion.LookRotation(lookDirection, Vector3.up);
                transform.rotation = rotate;
            }
            ChangeState(RunState);
        }
        else if(!isAttack)
        {
            rb.velocity = Vector3.zero;
            ChangeState(IdleState);
        }
    }
}
