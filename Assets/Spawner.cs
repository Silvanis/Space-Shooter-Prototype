using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

public class Spawner : MonoBehaviour
{
    public List<WaypointData> waypoints = new List<WaypointData>();
    public GameObject enemyType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
       
        Gizmos.DrawIcon(transform.position, "InactiveSpawn.png", true);

        if (waypoints != null)
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                Gizmos.DrawIcon(waypoints[i].waypoint, "InActiveWaypoint" + (i + 1) + ".png", true);
                Gizmos.color = Color.gray;

                if (i == 0)
                {
                    Gizmos.DrawLine(transform.position, waypoints[i].waypoint);
                }

                if (i + 1 < waypoints.Count)
                {
                    Gizmos.DrawLine(waypoints[i].waypoint, waypoints[i + 1].waypoint);
                }
            }
        }
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawIcon(transform.position, "ActiveSpawn.png", true);

        if (waypoints != null)
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                Gizmos.DrawIcon(waypoints[i].waypoint, "ActiveWaypoint" + (i + 1) + ".png", true);
                Gizmos.color = Color.blue;
                if (i == 0)
                {
                    Gizmos.DrawLine(transform.position, waypoints[i].waypoint);

                }

                if (i + 1 < waypoints.Count)
                {
                    Gizmos.DrawLine(waypoints[i].waypoint, waypoints[i + 1].waypoint);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "SpawnField")
        {
            Vector3 pos = transform.position;
            GameObject enemy = Instantiate(enemyType, pos, Quaternion.identity);
            enemy.GetComponent<EnemyBase>().waypoints = waypoints;
        }
    }


}
