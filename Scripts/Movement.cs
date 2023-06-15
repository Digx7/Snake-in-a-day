using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CustomMonoBehaviorWrapper
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 direction;
    public void setMoveDirection(Vector2 direction)
    {
        this.direction = direction;
    }
    [SerializeField] private Vector2 deadZone;
    [SerializeField] private Vector2EventChannelSO InputChannelSo;

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
        }
        else{
            rb.velocity = Vector2.zero;
        }
    }

}
