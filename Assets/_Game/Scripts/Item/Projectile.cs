using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : GameUnit
{
    private Character _Owner;
    private float range;
    private float speed;
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
    public void OnInit(Character owner, Vector3 direction, Weapons weapon)
    {
        _Owner = owner;
        range = weapon.range;
        speed = weapon.speed + 1f;
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
