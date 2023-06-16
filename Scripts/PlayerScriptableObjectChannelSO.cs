using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/Player Scriptable Object Channel")]
public class PlayerScriptableObjectChannelSO : ScriptableObject
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