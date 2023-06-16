using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/Player Data Channel")]
public class PlayerDataChannelSO : ScriptableObject
{
    public UnityAction<Player> OnEventRaised;

    public void RaiseEvent(Player player)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(player);
        }
    }
}