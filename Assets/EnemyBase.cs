using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public List<WaypointData> waypoints;
    protected float currentSpeed;
    protected Vector3 currentWaypoint;
    protected int waypointIndex;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentSpeed = waypoints[0].speed;
        currentWaypoint = waypoints[0].waypoint;
        waypointIndex = 0;
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
    }

    protected virtual void Movement()
    {
        float distance = Vector3.Distance(transform.position, currentWaypoint);
        if (distance < 0.1f )
        {
            if (waypointIndex < waypoints.Count - 1)
            {
                waypointIndex++;
                currentSpeed = waypoints[waypointIndex].speed;
                currentWaypoint = waypoints[waypointIndex].waypoint;
            }
            
        }
    }
}
