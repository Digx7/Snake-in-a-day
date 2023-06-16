using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CustomMonoBehaviorWrapper
{
    [SerializeField] bool rotateToFaceDirectionOfMovement = false;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;
    public void setMoveDirection(Vector2 direction)
    {
        this.direction = direction;
    }
    [SerializeField] private Vector2 deadZone;
    [SerializeField] private Vector2EventChannelSO InputChannelSo;
    public void setInputChannelSo(Vector2EventChannelSO inputChannelSo)
    {
        try
        {
           InputChannelSo.OnEventRaised -= (input) => setMoveDirection(input); 
        }
        catch (System.Exception)
        {

        }

        this.InputChannelSo = inputChannelSo;
        this.InputChannelSo.OnEventRaised += (input) => setMoveDirection(input);
    }

    private void OnEnable()
    {
        try
        {
            InputChannelSo.OnEventRaised += (input) => setMoveDirection(input);
        }
        catch (System.Exception)
        {
            
        }
    }

    private void OnDisable()
    {
        try
        {
            InputChannelSo.OnEventRaised -= (input) => setMoveDirection(input);
        }
        catch (System.Exception)
        {

        }
    }

    private void FixedUpdate()
    {
        if(direction.magnitude > deadZone.magnitude){
            rb.velocity = direction * speed;

            if(rotateToFaceDirectionOfMovement)
            {
                // Get the angle in degrees from the target direction
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                // Create a quaternion rotation around the Z-axis
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);

                // Apply the rotation to the object
                transform.rotation = targetRotation;
            }
        }
        else{
            rb.velocity = Vector2.zero;
        }
    }

}
