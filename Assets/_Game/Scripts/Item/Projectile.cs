using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : GameUnit
{
    private Character _Owner;
    private float range;
    private float bulletSpeed;
    public Rigidbody rb;
    private void Update()
    {
        if (range <= 0)
        {
            OnDespawn();
        }
        else
        {
            range -= bulletSpeed * Time.deltaTime;
        }
    }
    public void OnInit(Character owner, Vector3 direction, WeaponData weapon)
    {
        _Owner = owner;
        range = owner.attackRangeRadius - 5f;
        bulletSpeed = weapon.speed + 1f;
        rb.velocity = direction * bulletSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.CHARACTER_TAG))
        {
            OnDespawn();
            Cache.GetCharacter(other).Hit();
            _Owner.IncreasePoint();
        }
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
