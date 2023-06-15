using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : CustomMonoBehaviorWrapper
{
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private float followDistance;
    [SerializeField] private float followSpeed;

    private float distanceToTarget{get {return Vector3.Distance(objectToFollow.position, transform.position);}}

    private bool isMoving = false;

    private void Update()
    {
        if(!isMoving)
        {
            if(distanceToTarget > followDistance) StartCoroutine(follow());
        }
    }

    private IEnumerator follow()
    {
        
        while(distanceToTarget > followDistance)  // is the distance to the target greater than the follow distance?
        {
            isMoving = true;

            Log("Distance to target is " + distanceToTarget);

            Vector3 directionTowardsTarget = (objectToFollow.position - transform.position).normalized;  // get direction towards the target

            Log("Direction to target is " + directionTowardsTarget);

            Vector3 delta = (directionTowardsTarget * followSpeed);

            Log("Moved by: " + delta);

            transform.position += delta;  // move in that direction;

            yield return new WaitForFixedUpdate();
        }

        isMoving = false;
    }
}
