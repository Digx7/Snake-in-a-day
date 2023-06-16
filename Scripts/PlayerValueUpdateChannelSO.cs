using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/Player Value Update Channel")]
public class PlayerValueUpdateChannelSO : ScriptableObject
{
    public UnityAction<int,int> OnEventRaised;

    public void RaiseEvent(int ID, int value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(ID, value);
        }
    }
}