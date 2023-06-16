using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : CustomMonoBehaviorWrapper
{
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
