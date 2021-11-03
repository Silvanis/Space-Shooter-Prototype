using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ProjectileDelegate();
public abstract class ProjectileBase : MonoBehaviour
{
    public float bulletSpeed = 5.0f;
    protected SpriteRenderer m_renderer;
    public bool isVisible = true;

    public event ProjectileDelegate OnProjectileDestroyed;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_renderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += transform.right * bulletSpeed * Time.deltaTime;

    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            OnProjectileDestroyed.Invoke();
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SpawnField")
        {
            OnProjectileDestroyed.Invoke();
            Destroy(gameObject);
        }
    }
}
