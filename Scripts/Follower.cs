using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : CustomMonoBehaviorWrapper
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private float followDistance;
    [SerializeField] private float followSpeed;

    private void Update()
    {

        float distanceToTarget = Vector3.Distance(objectToFollow.position, transform.position);

        while(distanceToTarget > followDistance)  // is the distance to the target greater than the follow distance?
        {
            Log("Distance to target is " + distanceToTarget);

            Vector3 directionTowardsTarget = (objectToFollow.position - transform.position).normalized;  // get direction towards the target

            Log("Direction to target is " + directionTowardsTarget);

            Vector3 delta = (directionTowardsTarget * followSpeed * Time.deltaTime);

            Log("Moved by: " + delta);

            transform.position += delta;  // move in that direction;
        }
    }
}
