using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : CustomMonoBehaviorWrapper
{
    [SerializeField] private string fruitTag;
    [SerializeField] private string dangerTag;
    
    public UnityEvent OnRecieveFruit;
    public UnityEvent OnHit;
    
    private void OnCollisionEnter2D (Collision2D col)
    {
        string tag = col.gameObject.tag;

        if(tag == fruitTag)
        {
            OnRecieveFruit.Invoke();
        }
        else if(tag == dangerTag)
        {
            OnHit.Invoke();
        }
    }
}
