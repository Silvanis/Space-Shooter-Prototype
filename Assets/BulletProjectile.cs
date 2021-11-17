using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ProjectileDelegate();
public class BulletProjectile : ProjectileBase
{
    public event ProjectileDelegate OnBulletDestroyed;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            OnBulletDestroyed.Invoke();
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SpawnField")
        {
            OnBulletDestroyed.Invoke();
            Destroy(gameObject);
        }
    }
}
