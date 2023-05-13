using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public Projectile arrowPrefab;
    public Transform firePoint;

    public Character owner;
    public void Fire(Vector3 direction)
    {
        Projectile projectile = SimplePool.Spawn<Projectile>(arrowPrefab, firePoint.position, firePoint.rotation);
        projectile.OnInit(owner.baseAttackRange, direction);
    }
}
