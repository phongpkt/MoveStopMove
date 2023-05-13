using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public Projectile arrowPrefab;
    public Transform firePoint;
    public void Shoot(Character owner,Vector3 direction)
    {
        Projectile projectile = SimplePool.Spawn<Projectile>(arrowPrefab, firePoint.position, firePoint.rotation);
        projectile.OnInit(owner, direction);
    }
}
