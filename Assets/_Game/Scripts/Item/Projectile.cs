using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : GameUnit
{
    private Character _Owner;
    private float range;
    private float bulletSpeed;
    public Rigidbody rb;
    private Vector3 ulti = new Vector3(2f, 2f, 2f);
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
        if (_Owner.hasUlti)
        {
            transform.localScale += ulti * Time.deltaTime * 2.5f;
        }
    }
    public void OnInit(Character owner, Vector3 direction, WeaponData weapon)
    {
        _Owner = owner;
        //Remind: chinh phan nay de game de thang hon
        range = owner.attackRangeRadius - 6.5f;
        bulletSpeed = weapon.speed - 0.1f;
        rb.velocity = direction * bulletSpeed;
    }
    public void OnInitUlti(Character owner, Vector3 direction, WeaponData weapon)
    {
        _Owner = owner;
        //reset scale vu khi
        transform.localScale = new Vector3(1f, 1f, 1f);
        range = owner.attackRangeRadius - 5.5f;
        bulletSpeed = weapon.speed - 0.3f;
        rb.velocity = direction * bulletSpeed;
    }   
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.CHARACTER_TAG))
        {
            Character target = Cache.GetCharacter(other);
            if(target != _Owner)
            {
                OnDespawn();
                target.Hit();
                _Owner.IncreasePoint();
            }
        }
    }
    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }
}
