using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float lifetime = 5f;
    [SerializeField] protected string tagToHit;
    [SerializeField] protected LayerMask layersToHit;
    [SerializeField] protected ParticleSystem destroyParticle;

    [HideInInspector] public Vector2 direction;

    protected IObjectPool<Projectile> pool;
    protected SpriteRenderer projectileSprite;
    protected Rigidbody2D projectileRigidbody;

    public void SetPool(IObjectPool<Projectile> toSetPool) => pool = toSetPool;

    protected void Awake()
    {
        projectileSprite = this.GetComponentInChildren<SpriteRenderer>();
        projectileRigidbody = this.GetComponent<Rigidbody2D>();
    }

    protected virtual void OnEnable()
    {
        Invoke("Destroy", lifetime);
    }

    protected virtual void OnDisable()
    {
        CancelInvoke();
        destroyParticle.Stop();
    }

    protected void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        projectileRigidbody.velocity = direction.normalized * speed;
    }

    protected void Destroy()
    {
        StartCoroutine(Destroying());
    }

    protected IEnumerator Destroying()
    {
        if (destroyParticle != null)
        {
            destroyParticle.Play();
        }
        projectileSprite.enabled = false;

        yield return new WaitForSeconds(destroyParticle.main.duration);

        projectileSprite.enabled = true;
        pool.Release(this);
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

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        var collidedGameObject = other.gameObject;
        if (collidedGameObject.CompareTag(tagToHit))
        {
            collidedGameObject.GetComponent<LifeSystem>().Damage(damage);
        }
        Destroy();
    }

}
