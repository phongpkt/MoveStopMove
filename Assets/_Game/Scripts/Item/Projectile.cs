using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : GameUnit
{
    private float range;
    float speed = 3f;
    public Rigidbody rb;
    private void FixedUpdate()
    {
        if (range < 0)
        {
            OnDespawn();
        }
        else
        {
            range -= speed * Time.deltaTime;
        }
    }
    public void OnInit(Character onwer, Vector3 direction)
    {
        direction.y = 0;
        this.range = onwer.baseAttackRange;
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Character"))
        {
            OnDespawn();
        }
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
    public override void OnInit()
    {
        
    }

}
