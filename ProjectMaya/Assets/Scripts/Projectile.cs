using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public float lifetime = 5f;
    public LayerMask layersToHit;
    public Vector2 direction { get;  set; }
    
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
        gameObject.SetActive(false);
    }

    protected private void OnDisable() {
        CancelInvoke();
    }

    public abstract IEnumerator Modifier();


}
