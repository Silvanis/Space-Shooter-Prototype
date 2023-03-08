using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipInput : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;
    [SerializeField]
    private float speedupIncrement = 1.0f;
    [Tooltip("Max number of Speedups allowed")]
    [SerializeField]
    private int maxNumberOfSpeedups = 4; //counts initial stage
    private int speedupStage = 1;
    

    private Vector2 currentMoveDirection = new Vector2(0.0f, 0.0f);
    private Animator shipAnimationController;
    private bool movingVertical = false;
    private GameObject shipFlamesStage1;
    private GameObject shipFlamesStage2;
    private GameObject shipFlamesStage3;
    private GameObject shipFlamesStage4;

    private void OnEnable()
    {
        EventManager.StartListening("powerupSelected", OnSpeedupSelected);
    }

    private void OnDisable()
    {
        EventManager.StopListening("powerupSelected", OnSpeedupSelected);
    }

    // Start is called before the first frame update
    void Start()
    {
        shipAnimationController = GetComponentInChildren<Animator>();
        shipFlamesStage1 = transform.Find("ShipModel/SpeedStage1").gameObject;
        shipFlamesStage2 = transform.Find("ShipModel/SpeedStage2").gameObject;
        shipFlamesStage3 = transform.Find("ShipModel/SpeedStage3").gameObject;
        shipFlamesStage4 = transform.Find("ShipModel/SpeedStage4").gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = (Vector2)transform.position + (currentMoveDirection * moveSpeed * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.isWorldMoving)
        {
            transform.position += GameManager.Instance.worldMovementRate * Time.fixedDeltaTime * transform.right;
        }

    }


    public void OnSpeedupSelected(Dictionary<string, object> message)
    {
        if (message.ContainsKey("speedup") && speedupStage < maxNumberOfSpeedups)
        {
            speedupStage++;
            moveSpeed += speedupIncrement;
            switch (speedupStage)
            {
                case 2:
                    shipFlamesStage2.SetActive(true);
                    shipFlamesStage1.SetActive(false);
                    break;
                case 3:
                    shipFlamesStage3.SetActive(true);
                    shipFlamesStage2.SetActive(false);
                    break;
                case 4:
                    shipFlamesStage4.SetActive(true);
                    shipFlamesStage3.SetActive(false);
                    break;
                default:
                    break;
            }
        }
        
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        currentMoveDirection = context.ReadValue<Vector2>().normalized;
        if (Mathf.Abs(currentMoveDirection.y) > 0.1f) //moving up or down?
        {
            if (!movingVertical) //have we started moving up or down?
            {
                movingVertical = true;
                shipAnimationController.SetBool("stoppedMoving", false);
                if (currentMoveDirection.y > 0.5f)
                {
                    shipAnimationController.SetTrigger("Moving Up");
                }
                else
                {
                    shipAnimationController.SetTrigger("Moving Down");
                }
            }

        }
        else //not moving up or down
        {
            if (movingVertical)
            {
                movingVertical = false;
                shipAnimationController.SetBool("stoppedMoving", true);
            }
        }
        
        
    }

}
