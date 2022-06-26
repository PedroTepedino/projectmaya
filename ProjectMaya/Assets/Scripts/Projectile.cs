using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private string tagToHit;
    [SerializeField] private LayerMask layersToHit;
    [SerializeField] private ParticleSystem destroyParticle;

    [HideInInspector] public Vector2 direction;

    private IObjectPool<Projectile> pool;

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
    
    public virtual void OnCollisionEnter2D(Collision2D other) 
    {
        var collidedGameObject = other.gameObject;
        if (collidedGameObject.CompareTag(tagToHit))
        {
            collidedGameObject.GetComponent<LifeSystem>().Damage(damage);
        }
        Destroy();
    }

}
