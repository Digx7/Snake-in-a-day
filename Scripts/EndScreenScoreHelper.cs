using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenScoreHelper : CustomMonoBehaviorWrapper
{
    [SerializeField] private Player player;
    public void setPlayer (Player player)
    {
        this.player = player;
    }
    [SerializeField] private TextMeshProUGUI scoreTMP;
    public void setColor(Color color)
    {
        scoreTMP.color = color;
    }

    private void OnEnable()
    {
        RenderScore(player);
    }

    private void OnDisable()
    {
    
    }

    private void RenderScore(Player player)
    {
            scoreTMP.text = "Player " + player.ID + " - " + player.score;
    }
}
