﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManagement : MonoBehaviour
{
    public float fireRate = 0.5f;
    public GameObject bulletPrefab;
    public int maxNumOfBullets = 4;
    private int currentNumOfBullets = 0;
    private bool isShooting = false;
    private float fireTimerAccumulator = 0.0f;
    private List<BulletMovement> bulletList;
    private List<BulletMovement> removeList;

    // Start is called before the first frame update
    void Start()
    {
        bulletList = new List<BulletMovement>();
        removeList = new List<BulletMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting)
        {
            fireTimerAccumulator += Time.deltaTime;
            if (fireTimerAccumulator > fireRate && currentNumOfBullets < maxNumOfBullets)
            {
                Vector2 bulletPosition = new Vector2(this.transform.position.x + .25f, this.transform.position.y);
                var newBullet = Instantiate(bulletPrefab, bulletPosition, transform.rotation);
                bulletList.Add(newBullet.GetComponent<BulletMovement>());
                fireTimerAccumulator = 0.0f;
                currentNumOfBullets++;
            }
        }

    }

    private void FixedUpdate()
    {
        foreach (var bullet in bulletList)
        {
            if (!bullet.isVisible)
            {
                currentNumOfBullets--;
                removeList.Add(bullet);
            }
        }

        foreach (var bullet in removeList)
        {
            bulletList.Remove(bullet);
            Destroy(bullet.gameObject);
        }

        if (removeList.Count > 0)
        {
            removeList.Clear();
        }

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

    public void RemoveBullet()
    {
        currentNumOfBullets--;
    }
}