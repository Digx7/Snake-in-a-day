using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/Player Value Update Channel")]
public class PlayerValueUpdateChannelSO : ScriptableObject
{
    public UnityAction<Player,int> OnEventRaised;

    public void RaiseEvent(Player player, int value)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(player, value);
        }
    }
}