using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class WeaponBase : MonoBehaviour
{
    public int weaponPriority;
    public int projectileDamage;
    public int magazineSize;
    public int magazineRemaning;
    public float reloadTime;
    public float shootingSpeed;
    public Projectile projectilePrefab;

    public ObjectPool<Projectile> pool;

    protected void Awake() 
    {
        pool = new ObjectPool<Projectile>(CreateProjectile, OnTakeProjectileFromPool, OnReturnProjectileToPool);
    }

    Projectile CreateProjectile()
    {
        var projectile = Instantiate(projectilePrefab);
        projectile.SetPool(pool);
        return projectile;
    }

    void OnTakeProjectileFromPool(Projectile projectile)
    {
        projectile.transform.position = this.gameObject.transform.position;
        projectile.gameObject.SetActive(true);
    }

    void OnReturnProjectileToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    public abstract void Shoot();

    public virtual void Reload()
    {
        //wait reloadTime
        magazineRemaning = magazineSize;
    }

}
