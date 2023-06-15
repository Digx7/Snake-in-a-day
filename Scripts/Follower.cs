using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : CustomMonoBehaviorWrapper
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private float followDistance;
    [SerializeField] private float followSpeed;

    private void FixedUpdate()
    {
        while(Vector3.Distance(objectToFollow.position, transform.position) > followDistance)  // is the distance to the target greater than the follow distance?
        {
            Vector3 directionTowardsTarget = (objectToFollow.position - transform.position).normalized;  // get direction towards the target

            transform.position += (directionTowardsTarget * followSpeed);  // move in that direction;
        }
    }
}
