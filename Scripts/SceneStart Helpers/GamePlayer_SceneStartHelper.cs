using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer_SceneStartHelper : CustomMonoBehaviorWrapper
{
   [SerializeField] GameStateRequestChannelSO gameStateRequestChannelSO;
    
    private void Start()
    {
        gameStateRequestChannelSO.RaiseEvent(GameState.Gameplay);
    }
}
