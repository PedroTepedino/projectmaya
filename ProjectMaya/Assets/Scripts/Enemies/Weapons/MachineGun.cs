using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : WeaponBase
{
    public override void Shoot()
    {
        if (timerToShoot < 0 && magazineRemaning > 0)
        {
            var projectile = pool.Get();
            projectile.direction = this.transform.forward;
            magazineRemaning--;
        }
    }
}
