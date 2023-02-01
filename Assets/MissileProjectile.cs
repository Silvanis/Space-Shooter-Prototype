using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : ProjectileBase
{
    bool foundTarget = false;

    [SerializeField]
    private float fallingSpeed = 5.0f;
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (!foundTarget)
        {
            transform.position += Vector3.down * fallingSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += transform.right * bulletSpeed * Time.deltaTime;
        }
        
    }
}
