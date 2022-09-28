using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    private POWEUP_STAGE powerupStage = POWEUP_STAGE.NONE;
    [Header("Buttons")]
    [SerializeField]
    private Button speedupButton;
    [SerializeField]
    private Button missleButton;
    [SerializeField]
    private Button doubleButton;
    [SerializeField]
    private Button laserButton;
    [SerializeField]
    private Button cloneButton;
    [SerializeField]
    private Button forcefieldButton;
    [Header("Variables")]
    [Tooltip("Max number of Speedups allowed")]
    [SerializeField]
    private int maxNumberOfSpeedups = 3;
    private int currentNumberOfSpeedups = 0;

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
        switch (powerupStage) //set button on current stage to selected, make the previous one go back to highlighted
        {
            case POWEUP_STAGE.SPEEDUP: 
                speedupButton.animator.SetTrigger(speedupButton.animationTriggers.selectedTrigger);
                break;
            case POWEUP_STAGE.MISSLE:
                speedupButton.animator.SetTrigger(speedupButton.animationTriggers.highlightedTrigger);
                missleButton.animator.SetTrigger(missleButton.animationTriggers.selectedTrigger);
                break;
            case POWEUP_STAGE.DOUBLE:
                missleButton.animator.SetTrigger(missleButton.animationTriggers.highlightedTrigger);
                doubleButton.animator.SetTrigger(doubleButton.animationTriggers.selectedTrigger);
                break;
            case POWEUP_STAGE.LASER:
                doubleButton.animator.SetTrigger(doubleButton.animationTriggers.highlightedTrigger);
                laserButton.animator.SetTrigger(laserButton.animationTriggers.selectedTrigger);
                break;
            case POWEUP_STAGE.CLONE:
                laserButton.animator.SetTrigger(laserButton.animationTriggers.highlightedTrigger);
                cloneButton.animator.SetTrigger(cloneButton.animationTriggers.selectedTrigger);
                break;
            case POWEUP_STAGE.FORCEFIELD:
                cloneButton.animator.SetTrigger(cloneButton.animationTriggers.highlightedTrigger);
                forcefieldButton.animator.SetTrigger(forcefieldButton.animationTriggers.selectedTrigger);
                break;
            case POWEUP_STAGE.RESET:
                //reset all the powerups
                PowerupReset();
                OnPowerupCollect();
                break;
            default:
                break;
        }
    }

    private void PowerupReset()
    {
        powerupStage = POWEUP_STAGE.NONE;
        speedupButton.animator.SetTrigger(speedupButton.animationTriggers.normalTrigger);
        missleButton.animator.SetTrigger(missleButton.animationTriggers.normalTrigger);
        doubleButton.animator.SetTrigger(doubleButton.animationTriggers.normalTrigger);
        laserButton.animator.SetTrigger(laserButton.animationTriggers.normalTrigger);
        cloneButton.animator.SetTrigger(cloneButton.animationTriggers.normalTrigger);
        forcefieldButton.animator.SetTrigger(forcefieldButton.animationTriggers.normalTrigger);
        
    }

    public void OnPowerup(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            bool powerupUsed = false;
            switch (powerupStage)
            {
                case POWEUP_STAGE.NONE://you get NOTHING!
                    break;
                case POWEUP_STAGE.SPEEDUP:
                    if (speedupButton.interactable == true && currentNumberOfSpeedups < maxNumberOfSpeedups)
                    {
                        powerupUsed = true;
                        currentNumberOfSpeedups++;
                        EventManager.TriggerEvent("onSpeedupSelected");
                        if (currentNumberOfSpeedups == maxNumberOfSpeedups)
                        {
                            _ = speedupButton.interactable == false;
                        }
                    }
                    break;
                case POWEUP_STAGE.MISSLE:
                    break;
                case POWEUP_STAGE.DOUBLE:
                    break;
                case POWEUP_STAGE.LASER:
                    break;
                case POWEUP_STAGE.CLONE:
                    break;
                case POWEUP_STAGE.FORCEFIELD:
                    break;
                
                default:
                    break;
            }
            if (powerupUsed)
            {
                PowerupReset();
            }
        }
    }
}
public enum POWEUP_STAGE
{
    NONE,
    SPEEDUP,
    MISSLE,
    DOUBLE,
    LASER,
    CLONE,
    FORCEFIELD,
    RESET
}