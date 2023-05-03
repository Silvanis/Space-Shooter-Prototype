using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ProjectileBase : MonoBehaviour
{
    [SerializeField]
    protected float projectileSpeed = 5.0f;
    [SerializeField]
    private int projectileDamage = 1;

    public int ProjectileDamage { get => projectileDamage; protected set => projectileDamage = value; }




    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += projectileSpeed * Time.deltaTime * transform.right;

    }

    protected virtual void FixedUpdate()
    {

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SpawnField")
        {
            Destroy(gameObject);
        }
    }
}
