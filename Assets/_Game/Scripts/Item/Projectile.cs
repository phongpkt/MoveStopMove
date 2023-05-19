using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : GameUnit
{
    private float range;
    float speed = 3f;
    public Rigidbody rb;

    private Character _Owner;
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
    public void OnInit(Character owner, Vector3 direction)
    {
        direction.y = 0;
        _Owner = owner;
        range = _Owner.attackRange + 0.2f;
        rb.velocity = direction * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            OnDespawn();
            other.gameObject.GetComponent<Character>().Hit();
            _Owner.IncreasePoint();
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
