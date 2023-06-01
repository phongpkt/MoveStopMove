using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Projectile weaponPrefab;
    public Transform firePoint;
    public WeaponData _Weapon;
    public void Shoot(Character owner,Vector3 direction)
    {
        Projectile projectile = SimplePool.Spawn<Projectile>(weaponPrefab, firePoint.position, firePoint.rotation);
        projectile.OnInit(owner, direction, _Weapon);
    }


}
