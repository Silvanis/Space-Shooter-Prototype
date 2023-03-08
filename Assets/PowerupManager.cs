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
    private Button missileButton;
    [SerializeField]
    private Button doubleButton;
    [SerializeField]
    private Button laserButton;
    [SerializeField]
    private Button cloneButton;
    [SerializeField]
    private Button forcefieldButton;
    [SerializeField]
    private Button speedupButtonDisabled;
    [SerializeField]
    private Button missleButtonDisabled;
    [SerializeField]
    private Button doubleButtonDisabled;
    [SerializeField]
    private Button laserButtonDisabled;
    [SerializeField]
    private Button cloneButtonDisabled;
    [SerializeField]
    private Button forcefieldButtonDisabled;

    [Header("Powerup Limits")]
    [SerializeField]
    private int maxSpeedupStages;
    [SerializeField]
    private int maxMissileStages;
    [SerializeField]
    private int maxDoubleStages;
    [SerializeField]
    private int maxLaserStages;
    [SerializeField]
    private int maxCloneStages;
    [SerializeField]
    private int maxForcefieldStages;

    private int currentSpeedupStages = 0;
    private int currentMissileStages = 0;
    private int currentDoubleStages = 0;
    private int currentLaserStages = 0;
    private int currentCloneStages = 0;
    private int currentForcefieldStages = 0;

    private ShipInput ship;

    private Button currentSpeedupButton;
    private Button currentMissileButton;
    private Button currentDoubleButton;
    private Button currentLaserButton;
    private Button currentCloneButton;
    private Button currentForcefieldButton;
    // Start is called before the first frame update
    void Start()
    {
        ship = gameObject.GetComponent<ShipInput>();
        currentSpeedupButton = speedupButton;
        currentMissileButton = missileButton;
        currentDoubleButton = doubleButton;
        currentLaserButton = laserButton;
        currentCloneButton = cloneButton;
        currentForcefieldButton = forcefieldButton;
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

    public void OnPowerupCollect(Dictionary<string, object> message)
    {

        powerupStage++;
        Debug.Log("Gathered Powerup");
        switch (powerupStage) //set button on current stage to selected, make the previous one go back to highlighted
        {
            case POWEUP_STAGE.SPEEDUP:
                currentSpeedupButton.animator.SetTrigger(currentSpeedupButton.animationTriggers.selectedTrigger);
                break;
            case POWEUP_STAGE.MISSLE:
                currentSpeedupButton.animator.SetTrigger(currentSpeedupButton.animationTriggers.highlightedTrigger);
                currentMissileButton.animator.SetTrigger(currentMissileButton.animationTriggers.selectedTrigger);
                break;
            case POWEUP_STAGE.DOUBLE:
                currentMissileButton.animator.SetTrigger(currentMissileButton.animationTriggers.highlightedTrigger);
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
                OnPowerupCollect(null); //reset just clears the power stages, but we want this to "reset" to SpeedUp, so call to advance powerupStage
                break;
            default:
                break;
        }
    }

    private void PowerupReset()
    {
        //the animation for selecting a powerup needs time to play, so only reset the lower powerups

        switch (powerupStage)
        {
            case POWEUP_STAGE.FORCEFIELD:
                cloneButton.animator.SetTrigger(cloneButton.animationTriggers.normalTrigger);
                goto case POWEUP_STAGE.CLONE; //C# doesn't allow cass fallthough without an explicit goto
            case POWEUP_STAGE.CLONE:
                laserButton.animator.SetTrigger(laserButton.animationTriggers.normalTrigger);
                goto case POWEUP_STAGE.LASER;
            case POWEUP_STAGE.LASER:
                doubleButton.animator.SetTrigger(doubleButton.animationTriggers.normalTrigger);
                goto case POWEUP_STAGE.DOUBLE;
            case POWEUP_STAGE.DOUBLE:
                currentMissileButton.animator.SetTrigger(currentMissileButton.animationTriggers.normalTrigger);
                goto case POWEUP_STAGE.MISSLE;
            case POWEUP_STAGE.MISSLE:
                currentSpeedupButton.animator.SetTrigger(currentSpeedupButton.animationTriggers.normalTrigger);
                break;
            default:
                break;
        }
        powerupStage = POWEUP_STAGE.NONE;

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
                    if (currentSpeedupStages < maxSpeedupStages)
                    {
                        currentSpeedupButton.animator.SetTrigger(currentSpeedupButton.animationTriggers.pressedTrigger);
                        EventManager.TriggerEvent("powerupSelected", new Dictionary<string, object> { { "speedup", 1 } });
                        currentSpeedupStages++;
                        if (currentSpeedupStages >= maxSpeedupStages)
                        {
                            currentSpeedupButton = speedupButtonDisabled;
                            _ = StartCoroutine(OnDisableButton(POWEUP_STAGE.SPEEDUP));
                        }
                        powerupUsed = true;
                    }
                    break;
                case POWEUP_STAGE.MISSLE:
                    if (currentMissileStages < maxMissileStages)
                    {
                        currentMissileButton.animator.SetTrigger(currentMissileButton.animationTriggers.pressedTrigger);
                        EventManager.TriggerEvent("powerupSelected", new Dictionary<string, object> { { "missile", 1 } });
                        currentMissileStages++;
                        if (currentMissileStages >= maxMissileStages)
                        {
                            currentMissileButton = missleButtonDisabled;
                            _ = StartCoroutine(OnDisableButton(POWEUP_STAGE.MISSLE));
                        }
                        powerupUsed = true;
                    }
                    
                    break;
                case POWEUP_STAGE.DOUBLE:
                    EventManager.TriggerEvent("powerupSelected", new Dictionary<string, object> { { "double", 1 } });
                    break;
                case POWEUP_STAGE.LASER:
                    EventManager.TriggerEvent("powerupSelected", new Dictionary<string, object> { { "laser", 1 } });
                    break;
                case POWEUP_STAGE.CLONE:
                    EventManager.TriggerEvent("powerupSelected", new Dictionary<string, object> { { "clone", 1 } });
                    break;
                case POWEUP_STAGE.FORCEFIELD:
                    EventManager.TriggerEvent("powerupSelected", new Dictionary<string, object> { { "forcefield", 1 } });
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
    private IEnumerator OnDisableButton(POWEUP_STAGE buttonType)
    {
        //wait for 1 second before disabling button so animation has time to play
        yield return new WaitForSeconds(1);
        switch (buttonType)
        {
            case POWEUP_STAGE.NONE:
                break;
            case POWEUP_STAGE.SPEEDUP:
                speedupButton.gameObject.SetActive(false);
                speedupButtonDisabled.gameObject.SetActive(true);
                break;
            case POWEUP_STAGE.MISSLE:
                missileButton.gameObject.SetActive(false);
                missleButtonDisabled.gameObject.SetActive(true);
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