using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : CustomMonoBehaviorWrapper
{
    public UnityEvent OnHit;
    
    private void OnCollisionEnter2D (Collision2D col)
    {
        OnHit.Invoke();
    }
}
