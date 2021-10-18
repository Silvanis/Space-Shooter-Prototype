using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;

[System.Serializable]
public struct WaypointData
{
    public Vector3 waypoint;
    public PATH_TYPE movementStyle;
    public float speed;
}

public enum PATH_TYPE
{
    STRAIGHT,
    SINE_WAVE,
    SPIN
}
