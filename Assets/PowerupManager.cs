using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerupManager : MonoBehaviour
{
    private int powerupStage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventManager.StartListening("onPowerupCollect", OnPowerupCollect);
    }

    private void OnDisable()
    {
        EventManager.StopListening("onPowerupCollect", OnPowerupCollect);
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

    public void OnPowerup(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            Debug.Log("Use Powerup");
        }
    }
}
