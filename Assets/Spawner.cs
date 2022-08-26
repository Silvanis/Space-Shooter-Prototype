using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
[RequireComponent(typeof(Collider2D))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private List<WaypointData> waypoints = new List<WaypointData>();
    [Range(0.0f, 15.0f)]
    [SerializeField] private float spawnTime;
    
    [Header("Enemy Info")]
    [SerializeField] private GameObject enemyType;
    [Range(1,10)]
    [SerializeField] private int numOfEnemies;
    [SerializeField] private bool powerupEnemy;
    [SerializeField] private int powerupPositionInWave;
    [SerializeField] private Color powerupColor;
    private Vector3 oldPosition;

    public List<WaypointData> Waypoints { get => waypoints; set => waypoints = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        oldPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.hasChanged)
        {
            Vector3 changeInPosition = transform.position - oldPosition;
            for (int i = 0; i < waypoints.Count; i++)
            {
                WaypointData data = waypoints[i];
                data.waypoint += changeInPosition;
                waypoints[i] = data;
            }
            oldPosition = transform.position;

        }
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
            _ = StartCoroutine("SpawnEnemies");
        }
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 1; i <= numOfEnemies; i++)
        {
            Vector3 pos = transform.position;
            GameObject enemy = Instantiate(enemyType, pos, Quaternion.identity);
            enemy.GetComponent<EnemyBase>().waypoints = waypoints;
            if (i == powerupPositionInWave)
            {
                enemy.GetComponent<Renderer>().material.color = powerupColor;
                enemy.GetComponent<EnemyBase>().isPowerupEnemy = true;
            }
            yield return new WaitForSeconds(spawnTime);
        }
    }


}
