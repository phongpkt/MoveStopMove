using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FloatingJoystick joystick;
    private Vector3 direction;

    private void Update()
    {
        Moving();
        Attack();
        Die();
    }
    public override void Attack()
    {
        if (isAttack)
        {
            ChangeAnim(Constants.ANIM_ATTACK);
            //shoot projectile
            //Weapons weapon = GameManager.Instance.Pool.GetObject(weaponType).GetComponent<Weapons>();

        }
    }
    public override void Die()
    {
        if(isHit) 
        {
            ChangeAnim(Constants.ANIM_DEAD);
        }
    }
    public override void Moving()
    {
        if (Input.GetMouseButton(0))
        {
            horizontal = joystick.Horizontal;
            vertical = joystick.Vertical;
            direction = (Vector3.forward * vertical + Vector3.right * horizontal) * speed;
            rb.velocity = direction;
            Vector3 lookDirection = direction;

            if (Mathf.Abs(lookDirection.magnitude) > 0.01f)
            {

                Quaternion rotate = Quaternion.LookRotation(lookDirection, Vector3.up);
                transform.rotation = rotate;
            }
            isMoving = true;
            ChangeAnim(Constants.ANIM_RUN);
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
            rb.velocity = Vector3.zero;
            ChangeAnim(Constants.ANIM_IDLE);
        }
    }
}
