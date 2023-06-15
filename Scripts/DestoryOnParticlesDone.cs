using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryOnParticlesDone : CustomMonoBehaviorWrapper
{
    private ParticleSystem myParticleSystem;

    private void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!myParticleSystem.IsAlive(true))
        {
            Destroy(gameObject);
        }
    }
}
