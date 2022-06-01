using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private int powerupStage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPowerupCollect()
    {
        powerupStage++;
        Debug.Log("Gathered Powerup");
    }
}
