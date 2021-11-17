using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ProjectileBase : MonoBehaviour
{
    public float bulletSpeed = 5.0f;
   
    public bool isVisible = true;

    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
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
