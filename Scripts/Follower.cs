using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : CustomMonoBehaviorWrapper
{
    [SerializeField] private Movement movement;
    [SerializeField] private Transform objectToFollow;
    [SerializeField] private float followDistance;

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

            movement.setMoveDirection(directionTowardsTarget);

            yield return new WaitForFixedUpdate();
        }

        movement.setMoveDirection(Vector2.zero);

        isMoving = false;
    }
}
