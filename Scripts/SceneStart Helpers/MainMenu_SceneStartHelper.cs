using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_SceneStartHelper : CustomMonoBehaviorWrapper
{
    [SerializeField] private GameStateRequestChannelSO gameStateRequestChannelSO;

    private void Start()
    {
        gameStateRequestChannelSO.RaiseEvent(GameState.MainMenu);
    }
}
