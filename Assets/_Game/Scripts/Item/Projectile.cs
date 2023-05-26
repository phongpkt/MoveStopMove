using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : GameUnit
{
    private Character _Owner;
    [SerializeField] private float range;
    [SerializeField] private float speed;
    public Rigidbody rb;
    private void FixedUpdate()
    {
        if (range < _Owner.baseAttackRange)
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
        speed = weapon.speed;
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
