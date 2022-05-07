using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public float lifetime = 5f;
    public LayerMask layersToHit;
    public Vector2 direction { get;  set; }

    public ParticleSystem destroyParticle;

    IObjectPool<Projectile> pool;

    public void SetPool(IObjectPool<Projectile> toSetPool) => pool = toSetPool;
    
    protected void OnEnable() {
        Invoke("Destroy", lifetime);
    }

    protected void FixedUpdate() {
        Move();
    }

    public virtual void Move()
    {
        transform.Translate(direction * speed, Space.World);
    }

    protected void Destroy()
    {
        if (destroyParticle != null)
        {
            destroyParticle.Play();
        }
        pool.Release(this);
    }

    protected private void OnDisable() {
        CancelInvoke();
    }

    public abstract IEnumerator Modifier();


}
