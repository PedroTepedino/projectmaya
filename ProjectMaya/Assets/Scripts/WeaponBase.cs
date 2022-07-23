using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class WeaponBase : MonoBehaviour
{
    [SerializeField] protected int weaponPriority;
    public int WeaponPriority => weaponPriority;
    [SerializeField] protected int magazineSize;
    public int magazineRemaning {get; protected set;}
    [SerializeField] protected float reloadTime;
    [SerializeField] protected float shootingSpeed;
    [SerializeField] protected Projectile projectilePrefab;
    [SerializeField] protected ParticleSystem shootingParticle;
    [SerializeField] protected Transform spawnPosition;
    [SerializeField] protected int startingPoolSize = 20;
    [SerializeField] protected int maxPoolSize = 100;

    [HideInInspector] public ObjectPool<Projectile> pool {get; protected set;}
    [HideInInspector] public Vector2 aimDirection;

    protected float timerToShoot = 0;
    protected bool recharging = false;

    protected void Awake() 
    {
        pool = new ObjectPool<Projectile>(CreateProjectile, 
                                          OnTakeProjectileFromPool, 
                                          OnReturnProjectileToPool, 
                                          OnDestroyProjectileFromPool, 
                                          true, 
                                          startingPoolSize, 
                                          maxPoolSize);

        // for (int i = 0; i < startingPoolSize; i++)
        // {
        //     var projectile = CreateProjectile();
        //     projectile.gameObject.SetActive(false);
        // }

        magazineRemaning = magazineSize;
    }

    protected virtual void Update() {
        timerToShoot -= Time.deltaTime;
    }

    Projectile CreateProjectile()
    {
        var projectile = Instantiate(projectilePrefab);
        projectile.SetPool(pool);
        return projectile;
    }

    void OnTakeProjectileFromPool(Projectile projectile)
    {
        projectile.transform.position = spawnPosition.transform.position;
        projectile.gameObject.SetActive(true);
    }

    void OnReturnProjectileToPool(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    void OnDestroyProjectileFromPool(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }

    public abstract void Shoot();

    public virtual void Reload()
    {
        StartCoroutine(ReloadTimer());
    }

    private IEnumerator ReloadTimer()
    {
        recharging = true;
        yield return new WaitForSeconds(reloadTime);

        magazineRemaning = magazineSize;
        recharging = false;
    }


}