using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipInput : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    

    private Vector2 currentMoveDirection = new Vector2(0.0f, 0.0f);
    private Animator shipAnimationController;
    private bool movingVertical = false;
   

    // Start is called before the first frame update
    void Start()
    {
        shipAnimationController = GetComponentInChildren<Animator>();
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
            transform.position += transform.right * GameManager.Instance.worldMovementRate * Time.fixedDeltaTime;
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
