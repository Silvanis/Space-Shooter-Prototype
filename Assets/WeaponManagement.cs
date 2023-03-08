using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManagement : MonoBehaviour
{
    [SerializeField]
    private float bulletFireRate = 0.3f;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private int maxNumOfBullets = 4;
    private int currentNumOfBullets = 0;
    private bool isShooting = false;
    private bool isUsingBullets = true; //will need to test if using laser
    private float bulletFireTimerAccumulator = 0.0f;

    [SerializeField]
    private float missileFireRate = 2.0f;
    [SerializeField]
    private GameObject missilePrefab;
    [SerializeField]
    private bool missileAutofire = true;
    private bool isUsingMissiles = false;
    private float missileFireTimerAccumulator = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        EventManager.StartListening("powerupSelected", OnPowerupSelected);
    }

    private void OnDisable()
    {
        EventManager.StopListening("powerupSelected", OnPowerupSelected);
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting && isUsingBullets)
        {
            bulletFireTimerAccumulator += Time.deltaTime;
            if (bulletFireTimerAccumulator > bulletFireRate && currentNumOfBullets < maxNumOfBullets)
            {
                Vector2 bulletPosition = new Vector2(this.transform.position.x + .25f, this.transform.position.y);
                var newBullet = Instantiate(bulletPrefab, bulletPosition, transform.rotation);
                newBullet.GetComponent<BulletProjectile>().OnBulletDestroyed += WeaponManagement_OnBulletDestroyed;
                bulletFireTimerAccumulator = 0.0f;
                currentNumOfBullets++;
                
            }
        }

        if (isUsingMissiles && (missileAutofire = true || (missileAutofire = false && isShooting)))
        {
            missileFireTimerAccumulator += Time.deltaTime;
            if (missileFireTimerAccumulator > missileFireRate)
            {
                Vector2 missilePosition = new Vector2(this.transform.position.x, this.transform.position.y - 0.2f);
                var newMissile = Instantiate(missilePrefab, missilePosition, transform.rotation);
                missileFireTimerAccumulator = 0.0f;
                
            }
        }

    }

    private void WeaponManagement_OnBulletDestroyed()
    {
        currentNumOfBullets--;
    }

    private void FixedUpdate()
    {
 

    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            isShooting = true;
        }
        else if (context.canceled)
        {
            isShooting = false;
        }
    }

 
    private void OnPowerupSelected(Dictionary<string, object> message)
    {
        if (message.ContainsKey("missile"))
        {
            isUsingMissiles = true;
        }
    }

}
