using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : WeaponBase
{
    public override void Shoot()
    {
        var projectile = pool.Get();
        projectile.direction = this.transform.forward;
    }
}
