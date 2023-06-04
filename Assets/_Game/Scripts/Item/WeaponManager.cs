using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Projectile weaponPrefab;
    public WeaponData _Weapon;
    public void Shoot(Character owner, Vector3 direction)
    {
        Projectile projectile = SimplePool.Spawn<Projectile>(weaponPrefab, owner.firePoint.position, owner.firePoint.rotation);
        projectile.OnInit(owner, direction, _Weapon);
    }
    public void Ulti(Character owner, Vector3 direction)
    {
        Projectile projectile = SimplePool.Spawn<Projectile>(weaponPrefab, owner.firePoint.position, owner.firePoint.rotation);
        projectile.OnInitUlti(owner, direction, _Weapon);
    }

}
