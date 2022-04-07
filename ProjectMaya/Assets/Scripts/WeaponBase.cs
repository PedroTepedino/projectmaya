using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public int weaponPriority;
    public int projectileDamage;
    public int magazineSize;
    public int magazineRemaning;
    public float reloadTime;
    public float shootingSpeed;

    public abstract void Shoot();

    public virtual void Reload()
    {
        //wait reloadTime
        magazineRemaning = magazineSize;
    }

}
