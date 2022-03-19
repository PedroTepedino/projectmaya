using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public int damage;
    public float speed;
    public LayerMask layersToHit;

    public abstract IEnumerator Modifier();
}
