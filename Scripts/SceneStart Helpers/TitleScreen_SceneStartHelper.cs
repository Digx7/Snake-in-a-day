using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen_SceneStartHelper : MonoBehaviour
{
    [SerializeField] private Sound song;
    [SerializeField] private AudioRequestChannelSO musicRequestChannelSo;
    [SerializeField] private GameStateRequestChannelSO gameStateRequestChannelSO;

    private void Start()
    {
        musicRequestChannelSo.RaiseEvent(song);
        gameStateRequestChannelSO.RaiseEvent(GameState.TitleScreen);
    }
}
