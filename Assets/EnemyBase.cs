using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public List<WaypointData> waypoints;
    protected float currentSpeed;
    protected Vector3 currentWaypoint;
    protected int waypointIndex;
    public float initialFireDelay;
    public float subsequentFireDelay;
    protected float timeToFire;
    protected bool firedFirstShot;
    public GameObject projectilePrefab;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentSpeed = waypoints[0].speed;
        currentWaypoint = waypoints[0].waypoint;
        waypointIndex = 0;
        timeToFire = 0.0f;
        firedFirstShot = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float step = currentSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, step);
    }

    protected virtual void FixedUpdate()
    {
        Movement();
        timeToFire += Time.fixedDeltaTime;
        if (!firedFirstShot)
        {
            if (timeToFire > initialFireDelay)
            {
                Shoot();
                firedFirstShot = true;
                timeToFire = 0.0f;
            }
        }
        else if (timeToFire > subsequentFireDelay)
        {
            Shoot();
            timeToFire = 0.0f;
        }
    }

    protected virtual void Movement()
    {
        float distance = Vector3.Distance(transform.position, currentWaypoint);
        if (distance < 0.1f)
        {
            if (waypointIndex < waypoints.Count - 1)
            {
                waypointIndex++;
                currentSpeed = waypoints[waypointIndex].speed;
                currentWaypoint = waypoints[waypointIndex].waypoint;
            }

        }
    }

    protected virtual void Shoot()
    {
        _ = Instantiate(projectilePrefab, transform.position, transform.rotation);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SpawnField")
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player Weapons"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}

