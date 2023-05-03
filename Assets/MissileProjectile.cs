using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileProjectile : ProjectileBase
{
    private bool foundTarget = false;
    private Animator ani;
    private GameObject fireTrail;
    [SerializeField]
    private float fallingSpeed = 5.0f;
    [SerializeField]
    private float missileSpeedIncrement = 0.5f;
    // Start is called before the first frame update
    protected override void Start()
    {
        ani = gameObject.GetComponentInChildren<Animator>();
        fireTrail = transform.Find("MissileModel/MissileFireTrail").gameObject;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (!foundTarget)
        {
            LayerMask mask = LayerMask.GetMask("Enemies");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), Mathf.Infinity, mask);
            if (hit.collider != null)
            {
                foundTarget = true;
                ani.SetTrigger("FoundTarget");
                fireTrail.SetActive(true);
            }
        }
        else
        {
            projectileSpeed += missileSpeedIncrement;
        }

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
            transform.position += transform.right * projectileSpeed * Time.deltaTime;
        }
        
    }
}
