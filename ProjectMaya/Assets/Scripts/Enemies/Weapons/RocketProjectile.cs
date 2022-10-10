using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : Projectile
{
    protected float launchingTimer = 0f;

    protected override void OnEnable()
    {
        base.OnEnable();
        launchingTimer = 0f;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }
    protected void Update()
    {
        launchingTimer += Time.deltaTime;

        var rocketDirection = (direction - new Vector2(this.transform.position.x, this.transform.position.y)).normalized;
        var rotationAngle = Mathf.Atan2(rocketDirection.x, rocketDirection.y) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(rotationAngle, Vector3.forward), 1f);


        if (Vector3.Distance(this.transform.position, new Vector3(direction.x, direction.y, 0f)) < 0.1f)
        {
            Destroy();
        }
    }
    public override void Move()
    {

        if (launchingTimer > 0.2f)
        {
            var rocketDirection = (direction - new Vector2(this.transform.position.x, this.transform.position.y)).normalized;


            projectileRigidbody.velocity = rocketDirection * speed;
        }
        else
        {
            projectileRigidbody.velocity = Vector2.up * speed * launchingTimer;
        }
    }
    public override IEnumerator Modifier()
    {
        return null;
    }
}
