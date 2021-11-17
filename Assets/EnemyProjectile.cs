using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : ProjectileBase
{
    public Vector3 shipPosition;
    protected Vector3 firingAngle;
    // Start is called before the first frame update
    protected override void Start()
    {
        shipPosition = GameObject.Find("Ship").transform.position;
        firingAngle = (shipPosition - transform.position).normalized;
    }

    // Update is called once per frame
    protected override void Update()
    {
        transform.position += (firingAngle * bulletSpeed * Time.deltaTime);
    }


    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ship")
        {
            Debug.Log("Ship Hit");
            Destroy(gameObject);
        }
    }
}
