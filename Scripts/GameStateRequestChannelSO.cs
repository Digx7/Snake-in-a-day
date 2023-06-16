using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Channels/Game State Request Channel")]
public class GameStateRequestChannelSO : ScriptableObject
{
    public UnityAction<GameState> OnEventRaised;

    public void RaiseEvent(GameState state)
    {
        if (OnEventRaised != null)
        {
            OnEventRaised.Invoke(state);
        }
    }
}