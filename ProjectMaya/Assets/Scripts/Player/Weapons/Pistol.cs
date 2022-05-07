using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponBase
{
    public override void Shoot()
    {
        var projectile = pool.Get();
        projectile.direction = this.transform.forward; // trocar foward pela posição do player
    }
}
