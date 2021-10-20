using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipInput : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    

    private Vector2 currentMoveDirection = new Vector2(0.0f, 0.0f);
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = (Vector2)transform.position + (currentMoveDirection * moveSpeed * Time.deltaTime);
        if (GameManager.Instance.isWorldMoving)
        {
            transform.position += transform.right * GameManager.Instance.worldMovementRate * Time.deltaTime;
        }

    }

    private void FixedUpdate()
    {
        
        
    }



    public void OnMove(InputAction.CallbackContext context)
    {
        currentMoveDirection = context.ReadValue<Vector2>().normalized;
        
        
    }

}
